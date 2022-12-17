﻿namespace BugTrackerSU.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Services.Data.Project;
    using BugTrackerSU.Services.Data.Ticket;
    using BugTrackerSU.Services.Data.User;
    using BugTrackerSu.Web;
    using BugTrackerSU.Web.ViewModels.Tickets;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TicketsController : BaseController
    {
        private readonly IProjectService projectService;

        private readonly ITicketService ticketService;

        private readonly IUserService userService;

        public TicketsController(
            IUserService userService,
            IProjectService projectService,
            ITicketService ticketService)
        {
            this.projectService = projectService;
            this.ticketService = ticketService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdminManagerSubmiterRolesAuthorization)]
        public IActionResult Create(int id)
        {
            var userId = this.User.GetId();

            var model = new CreateTicketViewModel
            {
                AsignedProjectDevelopers = this.projectService.GetProjectAssignedDevelopers(id),
                ProjectId = id,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminManagerSubmiterRolesAuthorization)]
        public async Task<IActionResult> Create(int id, CreateTicketViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.AsignedProjectDevelopers = this.projectService.GetProjectAssignedUsers(id);

                return this.View(model);
            }

            var userId = this.User.GetId();

            model.ProjectId = id;

            await this.ticketService.CreateTicketAsync(model, userId);

            return this.Redirect("/Home/Index");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public IActionResult MyTickets(int id = 1)
        {
            var itemsPerPage = 5;

            var userRole = string.Empty;

            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                userRole = GlobalConstants.AdministratorRoleName;
            }
            else if (this.User.IsInRole(GlobalConstants.ProjectManagerRoleName))
            {
                userRole = GlobalConstants.ProjectManagerRoleName;
            }

            var userId = this.User.GetId();

            var model = new AllTicketsViewModel
            {
                PageNumber = id,
                Tickets = this.ticketService.GetAllUserTickets(userId, userRole, id, itemsPerPage),
                ItemsPerPage = itemsPerPage,
                ItemsCount = this.ticketService.GetUserTicketsCount(userId, userRole),
            };

            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public IActionResult Edit(int projectId, int ticketId)
        {
            var chekUser = this.ticketService.ChekIfUserIsAuthorizedToEdit(ticketId, this.User.GetId(), this.userService.GetUserRole(this.User));

            if (chekUser == false)
            {
                return this.Redirect("/");
            }

            var model = new EditTicketViewModel
            {
                AsignedProjectDevelopers = this.projectService.GetProjectAssignedDevelopers(projectId),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> Edit(int projectId, int ticketId, EditTicketViewModel model)
        {
            var userId = this.User.GetId();

            var chekUser = this.ticketService.ChekIfUserIsAuthorizedToEdit(ticketId, userId, this.userService.GetUserRole(this.User));

            if (chekUser == false)
            {
                return this.Redirect("/");
            }

            if (!this.ModelState.IsValid)
            {
                model.AsignedProjectDevelopers = this.projectService.GetProjectAssignedUsers(projectId);

                return this.View(model);
            }

            await this.ticketService.EditTicketAsync(model, userId);

            return this.Redirect($"/Tickets/Ticket?{ticketId}");
        }

        [HttpGet]
        public IActionResult Ticket(int id)
        {
            var model = this.ticketService.GetTicketDetailsById(id);

            return this.View(model);
        }
    }
}
