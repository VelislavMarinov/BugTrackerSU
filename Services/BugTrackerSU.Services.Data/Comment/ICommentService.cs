namespace BugTrackerSU.Services.Data.Comment
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Comments;

    public interface ICommentService
    {
        Task CreateTicketCommentAsync(CreateTicketCommentFormModel model, string userId);

        Task CreatePostCommentAsync(CreatePostCommentFormModel model, string userId);

        List<CommentViewModel> GetCommentsByTicketId(int ticketId, int pageNumber, int itemPerPage);

        List<CommentViewModel> GetCommentsByPostId(int postId, int pageNumber, int itemPerPage);
    }
}
