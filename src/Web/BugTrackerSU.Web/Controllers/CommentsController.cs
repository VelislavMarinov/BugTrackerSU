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

    public class CommentsController : BaseController
    {
        private readonly ICommentService commentService;
        private readonly IPostService postService;

        private readonly int itemsPerPage = PagingConstants.CommentsPagingItemsPerPage;

        public CommentsController(
            ICommentService commentService,
            IPostService postService)
        {
            this.commentService = commentService;
            this.postService = postService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AllRolesAuthorized)]
        public IActionResult PostComments(int id = 1, int postId = 0)
        {
            var model = this.commentService.GetCommentsByPostId(postId, id, this.itemsPerPage);
            model.ItemsPerPage = itemsPerPage;
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

            var userId = this.User.GetId();

            await this.commentService.CreatePostCommentAsync(model.CreatePostCommentFormModel, userId);

            return this.Redirect($"/Comments/PostComments?postId={model.CreatePostCommentFormModel.PostId}");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int commentId, int postId)
        {
            await this.commentService.DeleteCommentAsync(commentId);
            return this.RedirectToAction("PostComments", "Comments", new { postId = postId });
        }
    }
}
