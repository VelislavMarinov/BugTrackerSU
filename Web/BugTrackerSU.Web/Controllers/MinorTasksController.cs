namespace BugTrackerSU.Web.Controllers
{
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Services.Data.MinorTask;
    using BugTrackerSU.Services.Data.User;
    using BugTrackerSu.Web;
    using BugTrackerSU.Web.ViewModels.MinorTasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using BugTrackerSU.Services.Data.Ticket;

    public class MinorTasksController : BaseController
    {
        private readonly IMinorTaskService minorTaskService;

        private readonly IUserService userService;

        private readonly ITicketService ticketService;

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
        public IActionResult Create(int ticketId)
        {
            var chekUser = this.minorTaskService.ChekIfUserIsAuthorizedToCreateOrSeeTask(ticketId, this.User.GetId(), this.userService.GetUserRole(this.User));

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
            var userId = this.User.GetId();

            var chekUser = this.minorTaskService.ChekIfUserIsAuthorizedToCreateOrSeeTask(model.TicketId, userId, this.userService.GetUserRole(this.User));

            if (chekUser == false)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.minorTaskService.CreateMinorTaskAsync(model, userId);

            return this.Redirect("/MinorTasks/TicketTasks");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public IActionResult TicketTasks(int ticketId, int id = 1)
        {
            var itemsPerPage = 4;

            var userId = this.User.GetId();

            var chekUser = this.minorTaskService.ChekIfUserIsAuthorizedToCreateOrSeeTask(id, userId, this.userService.GetUserRole(this.User));

            if (chekUser == false)
            {
                return this.BadRequest();
            }

            var model = new AllMinorTaskViewModel()
            {
                TicketInfo = this.ticketService.GetTicketById(ticketId),
                TicketId = ticketId,
                PageNumber = id,
                ItemsPerPage = itemsPerPage,
                ItemsCount = this.minorTaskService.GetTicketTasksCount(ticketId),
                Tasks = this.minorTaskService.GetTicketTasksById(ticketId ,id, itemsPerPage),
            };

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> StartTask(int taskId)
        {
            await this.minorTaskService.StartTask(taskId);

            return this.Redirect("/Tickets/MyTickets");
        }

        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> FinishTask(int taskId)
        {
            await this.minorTaskService.StartTask(taskId);

            return this.Redirect("/Tickets/MyTickets");
        }

    }
}
