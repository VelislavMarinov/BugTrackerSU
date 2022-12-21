namespace BugTrackerSU.Services.Data.Article
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.Articles;

    public class ArticleService : IArticleService
    {

        private readonly IDeletableEntityRepository<Article> articleRepository;

        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public ArticleService(
            IDeletableEntityRepository<Article> articleRepository,
            IDeletableEntityRepository<Category> categoryRepository)
        {
            this.articleRepository = articleRepository;
            this.categoryRepository = categoryRepository;
        }

        public int ArticlesCountByCategoryId(int categoryId) => this.articleRepository.All().Where(x => x.CategoryId == categoryId).Count();

        public async Task CreateArticleAsync(CreateArticleFormModel model, string userId)
        {
            var article = new Article
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                VideoUrl = model.VideoUrl,
                AddedByUserId = userId,
                CategoryId = model.CategoryId,
            };

            await this.articleRepository.AddAsync(article);
            await this.articleRepository.SaveChangesAsync();
        }

        public async Task DeleteArticleAsync(int articleId)
        {
            var category = this.articleRepository
                .All()
                .Where(x => x.Id == articleId)
                .FirstOrDefault();

            this.articleRepository.Delete(category);
            await this.articleRepository.SaveChangesAsync();
        }

        public async Task EditArticleAsync(EditArticleFormModel model, int articleId)
        {
            var article = this.articleRepository
                .All()
                .Where(x => x.Id == articleId)
                .FirstOrDefault();
            article.Name = model.Name;
            article.Description = model.Description;
            article.ImageUrl = model.ImageUrl;
            article.VideoUrl = model.VideoUrl;
            article.CategoryId = model.CategoryId;

            this.articleRepository.Update(article);
            await this.articleRepository.SaveChangesAsync();
        }

        public AllArticlesViewModel GetAllArticlesInCategory(int categoryId, int pageNumber, int itemsPerPage)
        {
            var articles = this.articleRepository
                .All()
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Where(x => x.CategoryId == categoryId)
                .Select(x => new ArticleViewModel
                {
                    CreatedById = x.AddedByUserId,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    VideoUrl = x.VideoUrl,
                    CreatedBy = x.AddedByUser.UserName,
                    CategoryName = x.Category.Name,
                    Id = x.Id,
                })
                .ToList();

            var model = new AllArticlesViewModel
            {
                Categories = this.GetAllCategories(),
                ItemsCount = this.ArticlesCountByCategoryId(categoryId),
                PageNumber = pageNumber,
                ItemsPerPage = itemsPerPage,
                Articles = articles,
            };

            return model;
        }

        public AllArticlesViewModel GetAllArticles(int pageNumber, int itemsPerPage)
        {
            var articles = this.articleRepository
                .All()
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new ArticleViewModel
                {
                    CreatedById = x.AddedByUserId,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    VideoUrl = x.VideoUrl,
                    CreatedBy = x.AddedByUser.UserName,
                    CategoryName = x.Category.Name,
                    Id = x.Id,
                })
                .ToList();

            var model = new AllArticlesViewModel
            {
                Categories = this.GetAllCategories(),
                ItemsCount = this.ArticlesCount(),
                PageNumber = pageNumber,
                ItemsPerPage = itemsPerPage,
                Articles = articles,
            };

            return model;
        }

        public ICollection<ArticleCategoryViewModel> GetAllCategories()
        {
            var categories = this.categoryRepository
                .All()
                .Select(x => new ArticleCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToList();

            return categories;
        }

        public int ArticlesCount() => this.articleRepository.All().Count();

        public ArticleViewModel GetArticleById(int articleId)
        {
            var model = this.articleRepository
                .All()
                .Where(x => x.Id == articleId)
                .Select(x => new ArticleViewModel
                {
                    CreatedById = x.AddedByUserId,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    VideoUrl = x.VideoUrl,
                    CreatedBy = x.AddedByUser.UserName,
                    CategoryName = x.Category.Name,
                    Id = x.Id,
                })
                .FirstOrDefault();

            return model;
        }
    }
}
