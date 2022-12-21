namespace BugTrackerSU.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    public class AllPostsViewModel : PagingViewModel
    {
        public List<PostViewModel> Posts { get; set; }

    }
}
