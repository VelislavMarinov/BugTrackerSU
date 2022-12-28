namespace BugTrackerSU.Services.Data.Article
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Articles;

    public interface IArticleService
    {
        string GetEmbedYouTubeLink(string rawLink);

        Task<ICollection<ArticleCategoryViewModel>> GetAllCategories();

        Task CreateArticleAsync(CreateArticleFormModel model, string userId);

        Task<AllArticlesViewModel> GetAllArticlesInCategory(int categoryId, int pageNumber, int itemsPerPage);

        Task EditArticleAsync(EditArticleFormModel model, int articleId, string userId, string roleName);

        Task DeleteArticleAsync(int articleId, string userId, string userRole);

        Task<int> ArticlesCountByCategoryId(int categoryId);

        Task<int> ArticlesCount();

        Task<AllArticlesViewModel> GetAllArticles(int pageNumber, int itemsPerPage);

        Task<ArticleViewModel> GetArticleById(int articleId);
    }
}
