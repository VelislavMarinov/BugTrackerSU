namespace BugTrackerSU.Web.Areas.Administration.Controllers
{
    using BugTrackerSU.Services.Data;
    using BugTrackerSU.Services.Data.User;

    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        private readonly IUserService userService;

        public DashboardController(
            ISettingsService settingsService,
            IUserService userService)
        {
            this.settingsService = settingsService;
            this.userService = userService;
        }

        public IActionResult ManageUserRoles()
        {
            var model = this.userService.GetAllUsersAndRoles();
            return this.View(model);
        }
    }
}
