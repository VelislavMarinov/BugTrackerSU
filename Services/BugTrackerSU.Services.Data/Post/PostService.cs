namespace BugTrackerSU.Services.Data.Post
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.Comments;
    using BugTrackerSU.Web.ViewModels.Posts;

    public class PostService : IPostService
    {
        private readonly IDeletableEntityRepository<Post> postRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public PostService(
            IDeletableEntityRepository<Post> postRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Comment> commentRepository)
        {
            this.postRepository = postRepository;
            this.usersRepository = usersRepository;
            this.commentRepository = commentRepository;
        }

        public async Task CreatePostAsync(CreatePostViewModel model, string userId)
        {
            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                ProjectId = model.ProjectId,
                AddedByUserId = userId,
            };

            await this.postRepository.AddAsync(post);
            await this.postRepository.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int postId)
        {
            var post = this.postRepository
                .All()
                .Where(x => x.Id == postId)
                .FirstOrDefault();

            this.postRepository.Delete(post);
            await this.postRepository.SaveChangesAsync();
        }

        public PostViewModel GetPostById(int id)
        {
            var post = this.postRepository
               .All()
               .Where(x => x.Id == id)
               .Select(x => new PostViewModel
               {
                   Id = x.Id,
                   ProjectName = x.Project.Title,
                   Title = x.Title,
                   Content = x.Content,
                   AddedByUserId = x.AddedByUserId,
                   AddedByUserUserName = x.AddedByUser.UserName,
                   CreatedOn = x.CreatedOn,
               })
               .FirstOrDefault();

            return post;
        }

        public List<PostViewModel> GetPosts(int pageNumber, int itemsPerPage)
        {
            var post = this.postRepository
               .All()
               .OrderByDescending(x => x.Id)
               .Skip((pageNumber - 1) * itemsPerPage)
               .Take(itemsPerPage)
               .Select(x => new PostViewModel
               {
                   Id = x.Id,
                   ProjectName = x.Project.Title,
                   Title = x.Title,
                   Content = x.Content,
                   AddedByUserId = x.AddedByUserId,
                   AddedByUserUserName = x.AddedByUser.UserName,
                   CreatedOn = x.CreatedOn,
               })
               .ToList();

            return post;
        }

        public List<PostViewModel> GetPostsByProjectId(int projectId)
        {
            var post = this.postRepository
              .All()
              .Where(x => x.ProjectId == projectId)
              .Select(x => new PostViewModel
              {
                  Id = x.Id,
                  ProjectName = x.Project.Title,
                  Title = x.Title,
                  Content = x.Content,
                  AddedByUserId = x.AddedByUserId,
                  AddedByUserUserName = x.AddedByUser.UserName,
                  CreatedOn = x.CreatedOn,
              })
              .ToList();

            return post;
        }

        public int GetPostsCount() => this.postRepository.All().Count();
    }
}
