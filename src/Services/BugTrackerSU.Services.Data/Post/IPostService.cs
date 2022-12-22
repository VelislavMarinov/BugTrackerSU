namespace BugTrackerSU.Services.Data.Post
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Posts;

    public interface IPostService
    {
        Task<AllPostsViewModel> GetPosts(int pageNumber, int itemsPerPage);

        Task<List<PostViewModel>> GetPostsByProjectId(int projectId);

        Task DeletePostAsync(int postId, string userId, string roleName);

        Task CreatePostAsync(CreatePostFormModel model, string userId);

        Task EditPostAsync(EditPostFormModel model, string userId, string roleName);

        Task<PostViewModel> GetPostById(int id);

        Task<bool> ChekIfUserIsAuthorizedToEditPost(int postId, string userId, string roleName);

        Task<int> GetPostsCount();
    }
}
