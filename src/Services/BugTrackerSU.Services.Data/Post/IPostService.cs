namespace BugTrackerSU.Services.Data.Post
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BugTrackerSU.Web.ViewModels.Comments;
    using BugTrackerSU.Web.ViewModels.Posts;

    public interface IPostService
    {
        AllPostsViewModel GetPosts(int pageNumber, int itemsPerPage);

        List<PostViewModel> GetPostsByProjectId(int projectId);

        Task DeletePostAsync(int postId);

        Task CreatePostAsync(CreatePostViewModel model, string userId);

        PostViewModel GetPostById(int id);

        int GetPostsCount();
    }
}
