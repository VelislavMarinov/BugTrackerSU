namespace BugTrackerSU.Services.Data.Category
{
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Categories;

    public interface ICategoryService
    {
        Task Create(CreateCategoryFormModel model, string userId);
    }
}
