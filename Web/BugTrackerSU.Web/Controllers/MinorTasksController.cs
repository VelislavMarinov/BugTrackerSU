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

    public class MinorTasksController : BaseController
    {
        private readonly IMinorTaskService minorTaskService;

        private readonly IUserService userService;

        public MinorTasksController(
            IMinorTaskService minorTaskService,
            IUserService userService)
        {
            this.minorTaskService = minorTaskService;
            this.userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public IActionResult Create(int ticketId)
        {
            var chekUser = this.minorTaskService.ChekIfUserIsAuthorizedToCreateTask(ticketId, this.User.GetId(), this.userService.GetUserRole(this.User));

            if (chekUser == false)
            {
                return this.Redirect("/");
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

            var chekUser = this.minorTaskService.ChekIfUserIsAuthorizedToCreateTask(model.TicketId, userId, this.userService.GetUserRole(this.User));

            if (chekUser == false)
            {
                return this.Redirect("/");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.minorTaskService.CreateMinorTaskAsync(model, userId);

            return this.Redirect("/MinorTasks/TicketTasks");
        }
    }
}
