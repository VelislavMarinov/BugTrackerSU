namespace BugTrackerSU.Services.Data.Article
{
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Articles;

    public interface IArticleService
    {
        Task CreateArticleAsync(CreateArticleFormModel model, string userId);

        AllArticlesViewModel GetAllArticlesInCategory(int categoryId ,int pageNumber, int itemsPerPage);

        Task EditArticleAsync(EditArticleFormModel model, int articleId);

        Task DeleteArticleAsync(int articleId);

        int ArticlesCountByCategoryId(int categoryId);
    }
}
