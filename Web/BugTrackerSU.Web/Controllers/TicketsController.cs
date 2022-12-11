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
        private readonly IUserService userService;

        private readonly IProjectService projectService;

        private readonly ITicketService ticketService;

        public TicketsController(
            IUserService userService,
            IProjectService projectService,
            ITicketService ticketService)
        {
            this.userService = userService;
            this.projectService = projectService;
            this.ticketService = ticketService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.SubmitterRoleName)]
        public IActionResult Create(int id)
        {
            var userId = this.User.GetId();

            var model = new CreateTicketViewModel
            {
                AsignedProjectDevelopers = this.projectService.GetProjectAssignedDevelopers(id),
                ProjectId = id,
            };
            Console.WriteLine(model.AsignedProjectDevelopers.Count);

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.SubmitterRoleName)]
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
        public IActionResult MyTickets()
        {
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

            var model = this.ticketService.GetAllUserTickets(userId, userRole);

            return this.View(model);
        }
    }
}
