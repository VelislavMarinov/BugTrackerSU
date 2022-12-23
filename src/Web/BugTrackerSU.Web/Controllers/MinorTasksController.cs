namespace BugTrackerSU.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Services.Data.MinorTask;
    using BugTrackerSU.Services.Data.Ticket;
    using BugTrackerSU.Services.Data.User;
    using BugTrackerSu.Web;
    using BugTrackerSU.Web.ViewModels.MinorTasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class MinorTasksController : BaseController
    {
        private readonly IMinorTaskService minorTaskService;

        private readonly IUserService userService;

        private readonly ITicketService ticketService;

        private readonly int itemsPerPage = PagingConstants.MinorTasksPagingItemsPerPage;

        public MinorTasksController(
            IMinorTaskService minorTaskService,
            IUserService userService,
            ITicketService ticketService)
        {
            this.minorTaskService = minorTaskService;
            this.userService = userService;
            this.ticketService = ticketService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> Create(int ticketId)
        {
            var chekUser = await this.minorTaskService.ChekIfUserIsAuthorizedToCreateOrSeeTask(ticketId, this.User.GetId(), this.userService.GetUserRole(this.User));

            if (chekUser == false)
            {
                return this.BadRequest();
            }

            var model = new CreateMinorTaskFormModel();
            model.TicketId = ticketId;

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> Create(CreateMinorTaskFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.User.GetId();

            var chekUser = await this.minorTaskService.ChekIfUserIsAuthorizedToCreateOrSeeTask(model.TicketId, userId, this.userService.GetUserRole(this.User));

            if (chekUser == false)
            {
                return this.BadRequest();
            }

            try
            {
                await this.minorTaskService.CreateMinorTaskAsync(model, userId);

                return this.RedirectToAction("TicketTasks", "MinorTasks", new { ticketId = model.TicketId });
            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> TicketTasks(int ticketId, int id = 1)
        {
            var userId = this.User.GetId();

            var chekUser = await this.minorTaskService.ChekIfUserIsAuthorizedToCreateOrSeeTask(id, userId, this.userService.GetUserRole(this.User));

            var model = await this.minorTaskService.GetTicketTasksById(ticketId, id, this.itemsPerPage);

            if (chekUser == false)
            {
                return this.BadRequest();
            }

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> StartTask(int taskId, int ticketId)
        {
            await this.minorTaskService.StartTask(taskId);

            return this.RedirectToAction("TicketTasks", "MinorTasks", new { ticketId = ticketId });
        }

        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> FinishTask(int taskId, int ticketId)
        {
            await this.minorTaskService.FinishTask(taskId);

            return this.RedirectToAction("TicketTasks", "MinorTasks", new { ticketId = ticketId });
        }
    }
}
