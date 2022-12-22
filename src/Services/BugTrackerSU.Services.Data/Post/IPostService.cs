namespace BugTrackerSU.Services.Data.Post
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BugTrackerSU.Web.ViewModels.Posts;

    public interface IPostService
    {
        AllPostsViewModel GetPosts(int pageNumber, int itemsPerPage);

        List<PostViewModel> GetPostsByProjectId(int projectId);

        Task DeletePostAsync(int postId, string userId, string roleName);

        Task CreatePostAsync(CreatePostFormModel model, string userId);

        Task EditPostAsync(EditPostFormModel model, string userId);

        PostViewModel GetPostById(int id);

        bool ChekIfUserIsAuthorizedToEditPost(int postId, string userId, string roleName);

        int GetPostsCount();
    }
}
