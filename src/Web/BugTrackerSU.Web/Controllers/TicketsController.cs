namespace BugTrackerSU.Web.Controllers
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

        private readonly int itemsPerPage = PagingConstants.TicketsPagingItemsPerPage;

        public TicketsController(
            IUserService userService,
            IProjectService projectService,
            ITicketService ticketService)
        {
            this.projectService = projectService;
            this.ticketService = ticketService;
            this.userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdminManagerSubmiterRolesAuthorization)]
        public async Task<IActionResult> Create(int id)
        {
            var userId = this.User.GetId();

            var chekUser = await this.ticketService.ChekIfUserIsAuthorizedToCreateTicket(id, userId, this.userService.GetUserRole(this.User));

            if (chekUser == false)
            {
                return this.BadRequest();
            }

            var model = new CreateTicketViewModel
            {
                AsignedProjectDevelopers = await this.projectService.GetProjectAssignedDevelopers(id),
                ProjectId = id,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminManagerSubmiterRolesAuthorization)]
        public async Task<IActionResult> Create(int id, CreateTicketViewModel model)
        {
            var userId = this.User.GetId();

            var chekUser = await this.ticketService.ChekIfUserIsAuthorizedToCreateTicket(id, userId, this.userService.GetUserRole(this.User));

            if (chekUser == false)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                model.AsignedProjectDevelopers = await this.projectService.GetProjectAssignedUsers(id);

                return this.View(model);
            }

            try
            {
                model.ProjectId = id;

                await this.ticketService.CreateTicketAsync(model, userId);

                return this.Redirect("/Tickets/MyTickets");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> MyTickets(int id = 1)
        {
            var userRole = this.userService.GetUserRole(this.User);

            var userId = this.User.GetId();

            var model = await this.ticketService.GetAllUserTickets(userId, userRole, id, this.itemsPerPage);

            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> Edit(int projectId, int ticketId)
        {
            try
            {
                var chekUser = await this.ticketService.ChekIfUserIsAuthorizedToEdit(ticketId, this.User.GetId(), this.userService.GetUserRole(this.User));

                if (chekUser == false)
                {
                    return this.Forbid();
                }

                var model = new EditTicketViewModel
                {
                    AsignedProjectDevelopers = await this.projectService.GetProjectAssignedDevelopers(projectId),
                };

                return this.View(model);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> Edit(int projectId, int ticketId, EditTicketViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.AsignedProjectDevelopers = await this.projectService.GetProjectAssignedUsers(projectId);

                return this.View(model);
            }

            try
            {
                var userId = this.User.GetId();
                var userRole = this.userService.GetUserRole(this.User);

                await this.ticketService.EditTicketAsync(model, userId, userRole);

                return this.RedirectToAction("Ticket", "Tickets", new { id = ticketId });
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Ticket(int id)
        {
            var model = await this.ticketService.GetTicketDetailsById(id);

            return this.View(model);
        }
    }
}
