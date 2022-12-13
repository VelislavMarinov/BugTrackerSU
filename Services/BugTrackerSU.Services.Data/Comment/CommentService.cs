namespace BugTrackerSU.Services.Data.Comment
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.Comments;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public CommentService(IDeletableEntityRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task CreateCommentAsync(CreateCommentViewModel model, string userId)
        {
            var comment = new Comment
            {
                AddedByUserId = userId,
                Content = model.Content,
            };

            if (model.TicketId == null)
            {
                comment.PostId = (int)model.PostId;
            }
            else
            {
                comment.TicketId = (int)model.TicketId;
            }

            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public List<CommentViewModel> GetCommentsByPostId(int postId)
        {
            var model = this.commentRepository
                .All()
                .Where(x => x.PostId == postId)
                .Select(x => new CommentViewModel
                {
                    CommentId = x.Id,
                    TicketId = x.TicketId,
                    Content = x.Content,
                    CreatedOn = x.CreatedOn,
                    UserId = x.AddedByUserId,
                })
                .ToList();

            return model;
        }

        public List<CommentViewModel> GetCommentsByTicketId(int ticketId)
        {
            var model = this.commentRepository
                .All()
                .Where(x => x.TicketId == ticketId)
                .Select(x => new CommentViewModel
                {
                    CommentId = x.Id,
                    TicketId = x.TicketId,
                    Content = x.Content,
                    CreatedOn = x.CreatedOn,
                    UserId = x.AddedByUserId,
                })
                .ToList();

            return model;
        }
    }
}
