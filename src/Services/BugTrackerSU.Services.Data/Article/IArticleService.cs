namespace BugTrackerSU.Services.Data.Article
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Articles;

    public interface IArticleService
    {
        ICollection<ArticleCategoryViewModel> GetAllCategories();

        Task CreateArticleAsync(CreateArticleFormModel model, string userId);

        AllArticlesViewModel GetAllArticlesInCategory(int categoryId ,int pageNumber, int itemsPerPage);

        Task EditArticleAsync(EditArticleFormModel model, int articleId, string userId, string roleName);

        Task DeleteArticleAsync(int articleId, string userId, string userRole);

        int ArticlesCountByCategoryId(int categoryId);

        int ArticlesCount();

        public AllArticlesViewModel GetAllArticles(int pageNumber, int itemsPerPage);

        ArticleViewModel GetArticleById(int articleId);
    }
}
