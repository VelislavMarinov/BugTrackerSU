namespace BugTrackerSU.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using BugTrackerSU.Services.Data;
    using BugTrackerSU.Services.Data.User;
    using BugTrackerSU.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Mvc;

    public class RolesController : AdministrationController
    {
        private readonly IUserService userService;

        public RolesController(
            IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult ManageUserRoles()
        {
            var model = this.userService.GetAllUsersAndRoles();
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(ManageRolesViewModel model)
        {
            await this.userService.SetUserRole(model.UserId, model.RoleId);
            return this.Redirect("ManageUserRoles");
        }
    }
}
