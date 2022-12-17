namespace BugTrackerSU.Web.Controllers
{
    using BugTrackerSU.Services.Data.Comment;
    using BugTrackerSu.Web;
    using BugTrackerSU.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Mvc;
    using BugTrackerSU.Services.Data.Post;
    using System;
    using System.Threading.Tasks;

    public class CommentsController : BaseController
    {
        private readonly ICommentService commentService;
        private readonly IPostService postService;

        public CommentsController(
            ICommentService commentService,
            IPostService postService)
        {
            this.commentService = commentService;
            this.postService = postService;
        }

        [HttpGet]
        public IActionResult PostComments(int id = 1, int postId = 0)
        {
            var itemsPerPage = 3;

            Console.WriteLine(postId);

            var model = this.commentService.GetCommentsByPostId(postId, id, itemsPerPage);
            model.ItemsPerPage = itemsPerPage;
            model.PageNumber = id;
            model.ItemsCount = this.commentService.GetCommentsCountByPostId(postId);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostComment(PostCommentsViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/Comments/PostComments/1/{model.CreatePostCommentFormModel.PostId}");
            }

            var userId = this.User.GetId();

            await this.commentService.CreatePostCommentAsync(model.CreatePostCommentFormModel, userId);

            return this.Redirect($"/Comments/PostComments?postId={model.CreatePostCommentFormModel.PostId}");
        }
    }
}
