namespace BugTrackerSU.Services.Data.Post
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
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

        public Task CreatePostAsync(CreatePostViewModel model, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeletePostAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public PostViewModel GetPostById(int id)
        {
            throw new NotImplementedException();
        }

        public List<PostViewModel> GetPosts()
        {
            throw new NotImplementedException();
        }

        public List<PostViewModel> GetPostsByProjectId(int projectId)
        {
            throw new NotImplementedException();
        }
    }
}
