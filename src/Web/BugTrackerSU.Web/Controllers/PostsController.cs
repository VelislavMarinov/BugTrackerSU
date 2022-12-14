namespace BugTrackerSU.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Services.Data.Post;
    using BugTrackerSU.Services.Data.Project;
    using BugTrackerSU.Services.Data.User;
    using BugTrackerSu.Web;
    using BugTrackerSU.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : BaseController
    {
        private readonly IProjectService projectService;

        private readonly IPostService postService;

        private readonly int itemsPerPage = PagingConstants.PostsPagingItemsPerPage;

        private readonly IUserService userService;

        public PostsController(
            IProjectService projectService,
            IPostService postService,
            IUserService userService)
        {
            this.projectService = projectService;

            this.postService = postService;

            this.userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> Create()
        {
            var model = new CreatePostFormModel()
            {
                Projects = await this.projectService.GetAllProjects(),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> Create(CreatePostFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Projects = await this.projectService.GetAllProjects();

                return this.View(model);
            }

            try
            {
                var userId = this.User.GetId();

                await this.postService.CreatePostAsync(model, userId);

                return this.RedirectToAction("All", "Posts");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var userId = this.User.GetId();

                var roleName = this.userService.GetUserRole(this.User);

                if (!await this.postService.ChekIfUserIsAuthorizedToEditPost(id, userId, roleName))
                {
                    return this.Forbid();
                }

                var model = new EditPostFormModel();
                model.PostId = id;
                model.UserId = userId;

                return this.View(model);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> Edit(EditPostFormModel model, int postId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                var userId = this.User.GetId();
                var userRole = this.userService.GetUserRole(this.User);

                await this.postService.EditPostAsync(model, userId, userRole);
                return this.RedirectToAction("All", "Posts");
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> All(int id = 1)
        {
            var model = await this.postService.GetPosts(id, this.itemsPerPage);

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> Delete(int postId)
        {
            try
            {
                var userId = this.User.GetId();
                var userRole = this.userService.GetUserRole(this.User);

                await this.postService.DeletePostAsync(postId, userId, userRole);
                this.TempData["Message"] = "Post deleted successfully.";
                return this.RedirectToAction("All", "Posts");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

    }
}