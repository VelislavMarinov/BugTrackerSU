namespace BugTrackerSU.Web.ViewModels.Articles
{
    using System.Collections.Generic;

    using BugTrackerSU.Common;

    public class AllArticlesViewModel : PagingViewModel
    {
        public ICollection<ArticleViewModel> Articles { get; set; }

        public ICollection<ArticleCategoryViewModel> Categories { get; set; }

        public int CategoryId { get; set; }
    }
}
