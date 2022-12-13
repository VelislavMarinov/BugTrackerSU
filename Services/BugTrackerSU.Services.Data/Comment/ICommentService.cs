namespace BugTrackerSU.Services.Data.Comment
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Comments;

    public interface ICommentService
    {
        Task CreateCommentAsync(CreateCommentViewModel model, string userId);

        List<CommentViewModel> GetCommentsByTicketId(int ticketId);

        List<CommentViewModel> GetCommentsByPostId(int postId);
    }
}
