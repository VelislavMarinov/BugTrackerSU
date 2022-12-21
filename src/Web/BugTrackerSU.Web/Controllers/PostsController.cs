namespace BugTrackerSU.Web.Controllers
{
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Services.Data.Post;
    using BugTrackerSU.Services.Data.Project;
    using BugTrackerSu.Web;
    using BugTrackerSU.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public IActionResult Create()
        {
            var model = new CreatePostViewModel()
            {
                Projects = this.projectService.GetAllProjects(),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Projects = this.projectService.GetAllProjects();

                return this.View(model);
            }

            var userId = this.User.GetId();

            await this.postService.CreatePostAsync(model, userId);

            return this.Redirect("/Post/All");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public IActionResult All(int id = 1)
        {
            var itemsPerPage = 4;

            var model = new AllPostsViewModel()
            {
                PageNumber = id,
                ItemsPerPage = itemsPerPage,
                ItemsCount = this.postService.GetPostsCount(),
                Posts = this.postService.GetPosts(id, itemsPerPage),
            };

            return this.View(model);
        }
    }
}