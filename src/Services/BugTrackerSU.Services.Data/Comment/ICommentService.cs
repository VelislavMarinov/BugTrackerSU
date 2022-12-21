namespace BugTrackerSU.Services.Data.Comment
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Comments;

    public interface ICommentService
    {
        Task CreatePostCommentAsync(CreatePostCommentFormModel model, string userId);

        PostCommentsViewModel GetCommentsByPostId(int postId, int pageNumber, int itemPerPage);

        Task DeleteCommentAsync(int postId);

        int GetCommentsCountByPostId(int postId);
    }
}
