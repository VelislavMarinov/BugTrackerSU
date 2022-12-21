using System.Collections.Generic;

namespace BugTrackerSU.Web.ViewModels.Categories
{
    public class AllCategoriesViewModel : PagingViewModel
    {
        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}
