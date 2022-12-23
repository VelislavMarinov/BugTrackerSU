namespace BugTrackerSU.Services.Data.Post
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.Posts;
    using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> ChekIfUserIsAuthorizedToEditPost(int postId, string userId, string roleName)
        {
            if (roleName == GlobalConstants.AdministratorRoleName)
            {
                return true;
            }

            return await this.postRepository.All().AnyAsync(x => x.Id == postId && x.AddedByUserId == userId);
        }

        public async Task CreatePostAsync(CreatePostFormModel model, string userId)
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

        public async Task DeletePostAsync(int postId, string userId, string roleName)
        {
            if (roleName == GlobalConstants.AdministratorRoleName)
            {
                var adminPost = this.postRepository
                .All()
                .Where(x => x.Id == postId)
                .FirstOrDefault();

                if (adminPost == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    this.postRepository.Delete(adminPost);
                    await this.postRepository.SaveChangesAsync();
                }
            }
            else
            {
                var userPost = this.postRepository
                .All()
                .Where(x => x.Id == postId && x.AddedByUserId == userId)
                .FirstOrDefault();

                if (userPost == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    this.postRepository.Delete(userPost);
                    await this.postRepository.SaveChangesAsync();
                }
            }
        }

        public async Task EditPostAsync(EditPostFormModel model, string userId, string userRole)
        {
            if (userRole == GlobalConstants.AdministratorRoleName)
            {
                var adminPost = await this.postRepository
               .All()
               .Where(x => x.Id == model.PostId)
               .FirstOrDefaultAsync();

                if (adminPost == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    adminPost.Title = model.Title;
                    adminPost.Content = model.Content;

                    this.postRepository.Update(adminPost);
                    await this.postRepository.SaveChangesAsync();
                }
            }
            else
            {
                var userPost = this.postRepository
               .All()
               .Where(x => x.Id == model.PostId && x.AddedByUserId == userId)
               .FirstOrDefault();

                if (userPost == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    userPost.Title = model.Title;
                    userPost.Content = model.Content;

                    this.postRepository.Update(userPost);
                    await this.postRepository.SaveChangesAsync();
                }
            }
        }

        public async Task<PostViewModel> GetPostById(int id)
        {
            var post = await this.postRepository
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
               .FirstOrDefaultAsync();

            return post;
        }

        public async Task<AllPostsViewModel> GetPosts(int pageNumber, int itemsPerPage)
        {
            var post = await this.postRepository
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
               .ToListAsync();

            var model = new AllPostsViewModel()
            {
                PageNumber = pageNumber,
                ItemsPerPage = itemsPerPage,
                ItemsCount = await this.GetPostsCount(),
                Posts = post,
            };

            return model;
        }

        public async Task<List<PostViewModel>> GetPostsByProjectId(int projectId)
        {
            var post = await this.postRepository
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
              .ToListAsync();

            return post;
        }

        public async Task<int> GetPostsCount() => await this.postRepository.All().CountAsync();
    }
}
