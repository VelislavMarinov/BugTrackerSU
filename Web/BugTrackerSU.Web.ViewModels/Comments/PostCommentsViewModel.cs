namespace BugTrackerSU.Web.ViewModels.Comments
{
    using System.Collections.Generic;

    using BugTrackerSU.Web.ViewModels.Posts;

    public class PostCommentsViewModel : PagingViewModel
    {
        public PostViewModel PostViewModel { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public CreatePostCommentFormModel CreatePostCommentFormModel { get; set; }
    }
}
