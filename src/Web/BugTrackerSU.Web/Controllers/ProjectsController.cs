namespace BugTrackerSU.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Services.Data.Project;
    using BugTrackerSU.Services.Data.User;
    using BugTrackerSu.Web;
    using BugTrackerSU.Web.ViewModels.Projects;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ProjectsController : BaseController
    {
        private readonly IUserService userService;

        private readonly IProjectService projectService;

        private readonly int itemsPerPage = PagingConstants.ProjectsPagingItemsPerPage;

        public ProjectsController(
            IUserService userService,
            IProjectService projectService)
        {
            this.userService = userService;
            this.projectService = projectService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdminProjectMangerRolesAuthorization)]
        public IActionResult Create()
        {
            var users = this.userService.GetAllUsersAndRoles().Users;
            var model = new CreateProjectViewModel();
            model.AllUsers = users;

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminProjectMangerRolesAuthorization)]
        public async Task<IActionResult> Create(CreateProjectViewModel model)
        {

            if (!model.UserIds.Any())
            {
                model.AllUsers = this.userService.GetAllUsersAndRoles().Users;

                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                model.AllUsers = this.userService.GetAllUsersAndRoles().Users;

                return this.View(model);
            }

            var userId = this.User.GetId();

            await this.projectService.CreateProjectAsync(model, userId);

            return this.Redirect("/Home/Index");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public IActionResult MyProjects(int id = 1)
        {
            var userId = this.User.GetId();

            var userRole = this.userService.GetUserRole(this.User);

            var model = this.projectService.GetUserProjects(userId, userRole, id, this.itemsPerPage);

            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public IActionResult Project(int id)
        {
            if (!this.projectService.ChekIfProjectIsValid(id))
            {
                return this.BadRequest();
            }

            var projectDetails = this.projectService.GetProjectDetails(id);

            return this.View(projectDetails);
        }
    }
}
