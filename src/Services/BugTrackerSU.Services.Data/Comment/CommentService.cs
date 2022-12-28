namespace BugTrackerSU.Services.Data.Comment
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Services.Data.Post;
    using BugTrackerSU.Web.ViewModels.Comments;
    using Microsoft.EntityFrameworkCore;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;
        private readonly IPostService postService;

        public CommentService(
            IDeletableEntityRepository<Comment> commentRepository,
            IPostService postService)
        {
            this.commentRepository = commentRepository;
            this.postService = postService;
        }

        public async Task CreatePostCommentAsync(CreatePostCommentFormModel model, string userId)
        {
            var comment = new Comment
            {
                AddedByUserId = userId,
                Content = model.Content,
                PostId = model.PostId,
            };

            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int commentId, string userId, string roleName)
        {
            if (roleName == GlobalConstants.AdministratorRoleName)
            {
                var adminComment = this.commentRepository
                   .All()
                   .Where(x => x.Id == commentId)
                   .FirstOrDefault();

                if (adminComment == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    this.commentRepository.Delete(adminComment);
                    await this.commentRepository.SaveChangesAsync();
                }
            }
            else
            {
                var userComment = this.commentRepository
               .All()
               .Where(x => x.Id == commentId && x.AddedByUserId == userId)
               .FirstOrDefault();

                if (userComment == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    this.commentRepository.Delete(userComment);
                    await this.commentRepository.SaveChangesAsync();
                }
            }
        }

        public async Task<PostCommentsViewModel> GetCommentsByPostId(int postId, int pageNumber, int itemsPerPage)
        {
            var comments = await this.commentRepository
                .All()
                .Where(x => x.PostId == postId)
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new CommentViewModel
                {
                    CommentId = x.Id,
                    Content = x.Content,
                    CreatedOn = x.CreatedOn,
                    AddedByUserId = x.AddedByUserId,
                    PostId = x.PostId,
                    UserName = x.AddedByUser.UserName,
                })
                .ToListAsync();

            var model = new PostCommentsViewModel
            {
                Comments = comments,
                PostViewModel = await this.postService.GetPostById(postId),
                CreatePostCommentFormModel = new CreatePostCommentFormModel
                {
                    PostId = postId,
                },
            };

            return model;
        }

        public async Task<int> GetCommentsCountByPostId(int postId) => await this.commentRepository.All().Where(x => x.PostId == postId).CountAsync();
    }
}
