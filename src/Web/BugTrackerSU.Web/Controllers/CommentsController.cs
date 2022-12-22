namespace BugTrackerSU.Web.Controllers
{
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Services.Data.Comment;
    using BugTrackerSU.Services.Data.Post;
    using BugTrackerSu.Web;
    using BugTrackerSU.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using BugTrackerSU.Services.Data.User;

    public class CommentsController : BaseController
    {
        private readonly ICommentService commentService;

        private readonly IPostService postService;

        private readonly IUserService userService;

        private readonly int itemsPerPage = PagingConstants.CommentsPagingItemsPerPage;

        public CommentsController(
            ICommentService commentService,
            IPostService postService,
            IUserService userService)
        {
            this.commentService = commentService;
            this.postService = postService;
            this.userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public IActionResult PostComments(int id = 1, int postId = 0)
        {
            var model = this.commentService.GetCommentsByPostId(postId, id, this.itemsPerPage);
            model.ItemsPerPage = this.itemsPerPage;
            model.PageNumber = id;
            model.ItemsCount = this.commentService.GetCommentsCountByPostId(postId);

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public async Task<IActionResult> CreatePostComment(PostCommentsViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/Comments/PostComments?postId={model.CreatePostCommentFormModel.PostId}");
            }

            try
            {
                var userId = this.User.GetId();

                await this.commentService.CreatePostCommentAsync(model.CreatePostCommentFormModel, userId);

                return this.Redirect($"/Comments/PostComments?postId={model.CreatePostCommentFormModel.PostId}");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int commentId, int postId)
        {
            try
            {
                var userId = this.User.GetId();
                var userRole = this.userService.GetUserRole(this.User);

                await this.commentService.DeleteCommentAsync(commentId, userId, userRole);
                return this.RedirectToAction("PostComments", "Comments", new { postId = postId });
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
