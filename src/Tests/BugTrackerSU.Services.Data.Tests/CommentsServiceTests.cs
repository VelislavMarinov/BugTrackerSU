namespace BugTrackerSU.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BugTrackerSU.Common;
    using BugTrackerSU.Data;
    using BugTrackerSU.Data.Models; 
    using BugTrackerSU.Data.Repositories;
    using BugTrackerSU.Services.Data.Article;
    using BugTrackerSU.Services.Data.Comment;
    using BugTrackerSU.Services.Data.Post;
    using BugTrackerSU.Web.ViewModels.Comments;
    using Xunit;

    public class CommentsServiceTests : BaseServicesTests
    {
        [Fact]
        public async Task CreatePostCommentAsyncShouldWorkCorrectly()
        {
            ApplicationDbContext db = GetDb();
            var commentRepository = new EfDeletableEntityRepository<Comment>(db);
            var postRepository = new EfDeletableEntityRepository<Post>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var postService = new PostService(postRepository,userRepository, commentRepository);
            var commentService = new CommentService(commentRepository, postService);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            var project = new Project
            {
                Id = 2,
                Title = "test123",
                Description = "test123321",
                ProjectManagerId = user1.Id,
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            List<ApplicationUserProject> projectUsers = new List<ApplicationUserProject>
            {
                new ApplicationUserProject
                {
                  ProjectId = 2,
                  ApplicationUserId = user1.Id,
                },
            };

            project.ProjectUsers = projectUsers;

            await db.Projects.AddAsync(project);
            await db.SaveChangesAsync();

            var post = new Post
            {
                Id = 1,
                Title = "post123",
                Content = "content123",
                ProjectId = project.Id,
                AddedByUserId = user1.Id,
            };

            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();

            var model = new CreatePostCommentFormModel
            {
                Content = "Deja vuu iv been here before!!",
                PostId = post.Id,
            };

            await commentService.CreatePostCommentAsync(model, user1.Id);

            var result = commentRepository.All()
                .Where(x => x.Content == model.Content).FirstOrDefault();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetCommentsByPostIdShouldWorkCorrectly()
        {
            ApplicationDbContext db = GetDb();
            var commentRepository = new EfDeletableEntityRepository<Comment>(db);
            var postRepository = new EfDeletableEntityRepository<Post>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var postService = new PostService(postRepository, userRepository, commentRepository);
            var commentService = new CommentService(commentRepository, postService);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            var project = new Project
            {
                Id = 2,
                Title = "test123",
                Description = "test123321",
                ProjectManagerId = user1.Id,
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            List<ApplicationUserProject> projectUsers = new List<ApplicationUserProject>
            {
                new ApplicationUserProject
                {
                  ProjectId = 2,
                  ApplicationUserId = user1.Id,
                },
            };

            project.ProjectUsers = projectUsers;

            await db.Projects.AddAsync(project);
            await db.SaveChangesAsync();

            var post = new Post
            {
                Id = 1,
                Title = "post123",
                Content = "content123",
                ProjectId = project.Id,
                AddedByUserId = user1.Id,
            };

            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();

            var model = new CreatePostCommentFormModel
            {
                Content = "Deja vuu iv been here before!!",
                PostId = post.Id,
            };

            await commentService.CreatePostCommentAsync(model, user1.Id);
            await commentService.CreatePostCommentAsync(model, user1.Id);
            await commentService.CreatePostCommentAsync(model, user1.Id);
            await commentService.CreatePostCommentAsync(model, user1.Id);

            var result = await commentService.GetCommentsByPostId(post.Id, 1, 10);
            var count = await commentService.GetCommentsCountByPostId(post.Id);
            Assert.True(result.Comments.Count() == 4);
            Assert.True(count == 4);
        }

        [Fact]
        public async Task DeleteCommentAsyncShouldWorkCorrectly()
        {
            ApplicationDbContext db = GetDb();
            var commentRepository = new EfDeletableEntityRepository<Comment>(db);
            var postRepository = new EfDeletableEntityRepository<Post>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var postService = new PostService(postRepository, userRepository, commentRepository);
            var commentService = new CommentService(commentRepository, postService);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            var project = new Project
            {
                Id = 2,
                Title = "test123",
                Description = "test123321",
                ProjectManagerId = user1.Id,
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            List<ApplicationUserProject> projectUsers = new List<ApplicationUserProject>
            {
                new ApplicationUserProject
                {
                  ProjectId = 2,
                  ApplicationUserId = user1.Id,
                },
            };

            project.ProjectUsers = projectUsers;

            await db.Projects.AddAsync(project);
            await db.SaveChangesAsync();

            var post = new Post
            {
                Id = 1,
                Title = "post123",
                Content = "content123",
                ProjectId = project.Id,
                AddedByUserId = user1.Id,
            };

            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();

            var model = new CreatePostCommentFormModel
            {
                Content = "Deja vuu iv been here before!!",
                PostId = post.Id,
            };

            await commentService.CreatePostCommentAsync(model, user1.Id);

            var commentId = commentRepository.All().Where(x => x.Content == model.Content).FirstOrDefault().Id;

            await commentService.DeleteCommentAsync(commentId, user1.Id, GlobalConstants.AdministratorRoleName);

            var result = commentRepository.All().Where(x => x.Id == commentId).FirstOrDefault();

            Assert.Null(result);
        }
    }
}
