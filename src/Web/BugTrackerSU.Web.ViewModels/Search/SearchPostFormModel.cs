namespace BugTrackerSU.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using BugTrackerSU.Web.ViewModels.Posts;

    public class SearchPostFormModel
    {
        public string Keyword { get; set; }

        public IEnumerable<PostViewModel> Posts { get; set; }
    }
}
