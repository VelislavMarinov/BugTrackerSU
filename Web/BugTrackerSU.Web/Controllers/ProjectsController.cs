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

        public ProjectsController(
            IUserService userService,
            IProjectService projectService)
        {
            this.userService = userService;
            this.projectService = projectService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.ProjectManagerRoleName)]
        public IActionResult Create()
        {
            var users = this.userService.GetAllUsersAndRoles().Users;
            var model = new CreateProjectViewModel();
            model.AllUsers = users;

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.ProjectManagerRoleName)]
        public async Task<IActionResult> Create(CreateProjectViewModel model)
        {
            if (!model.AllUsers.Any(x => x.Selected))
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

            if(model.AllUsers.Any(x => x.Selected == true))
            {
                Console.WriteLine("Hello");
            }

            //await this.projectService.CreateProjectAsync(model, userId);

            return this.Redirect("/Home/Index");
        }

        [HttpGet]
        public IActionResult MyProjects(int id = 1)
        {
            var itemsPerPage = 5;

            var userId = this.User.GetId();

            var userRole = string.Empty;

            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                userRole = GlobalConstants.AdministratorRoleName;
            }

            var model = new AllProjectsViewModel
            {
                PageNumber = id,
                Projects = this.projectService.GetUserProjects(userId, userRole, id, itemsPerPage),
                ItemsPerPage = itemsPerPage,
                ItemsCount = this.projectService.GetUserProjectsCount(userId, userRole),
            };

            return this.View(model);
        }

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
