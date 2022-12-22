namespace BugTrackerSU.Services.Data.Category
{
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Categories;

    public interface ICategoryService
    {
        Task CreateCategoryAsync(CreateCategoryFormModel model, string userId);

        AllCategoriesViewModel GetAllCategories(int pageNumber, int itemsPerPage);

        Task EditCategoryAsync(EditCategoryFormModel model, int categoryId);

        Task DeleteCategoryAsync(int categoryId, string userId, string userRole);

        int CategoriesCount();
    }
}
