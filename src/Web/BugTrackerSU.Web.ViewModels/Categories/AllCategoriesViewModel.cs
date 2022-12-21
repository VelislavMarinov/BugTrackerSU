namespace BugTrackerSU.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    public class AllCategoriesViewModel : PagingViewModel
    {
        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}
