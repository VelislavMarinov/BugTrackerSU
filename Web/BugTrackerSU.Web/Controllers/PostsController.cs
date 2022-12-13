namespace BugTrackerSU.Web.Controllers
{
    using System.Threading.Tasks;
    using BugTrackerSu.Web;
    using BugTrackerSU.Services.Data.Post;
    using BugTrackerSU.Services.Data.Project;
    using BugTrackerSU.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : BaseController
    {
        private readonly IProjectService projectService;

        private readonly IPostService postService;

        public PostsController(
            IProjectService projectService,
            IPostService postService)
        {
            this.projectService = projectService;

            this.postService = postService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreatePostViewModel()
            {
                Projects = this.projectService.GetAllProjects(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Projects = this.projectService.GetAllProjects();

                return this.View(model);
            }

            var userId = this.User.GetId();

            await this.postService.CreatePostAsync(model, userId);

            return this.Redirect("/Home/Index");
        }
    }
}
