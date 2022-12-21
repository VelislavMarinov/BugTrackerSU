namespace BugTrackerSU.Services.Data.Comment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Services.Data.Post;
    using BugTrackerSU.Web.ViewModels.Comments;

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

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = this.commentRepository
               .All()
               .Where(x => x.Id == commentId)
               .FirstOrDefault();

            this.commentRepository.Delete(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public PostCommentsViewModel GetCommentsByPostId(int postId, int pageNumber, int itemsPerPage)
        {
            var comments = this.commentRepository
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
                .ToList();

            var model = new PostCommentsViewModel
            {
                Comments = comments,
                PostViewModel = this.postService.GetPostById(postId),
                CreatePostCommentFormModel = new CreatePostCommentFormModel
                {
                    PostId = postId,
                },
            };

            return model;
        }

        public int GetCommentsCountByPostId(int postId) => this.commentRepository.All().Where(x => x.PostId == postId).Count();
    }
}
