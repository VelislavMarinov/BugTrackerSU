namespace BugTrackerSU.Web.ViewModels.Articles
{
    using System.Collections.Generic;

    public class AllArticlesViewModel : PagingViewModel
    {
        public ICollection<ArticleViewModel> Articles { get; set; }
    }
}
