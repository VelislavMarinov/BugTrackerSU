namespace BugTrackerSU.Services.Data.Tests
{
    using BugTrackerSU.Data;
    using BugTrackerSU.Data.Repositories;
    using BugTrackerSU.Data.Models;
    using System;
    using Xunit;
    using BugTrackerSU.Services.Data.Article;
    using System.Threading.Tasks;
    using BugTrackerSU.Services.Data.Category;
    using System.Collections.Generic;
    using BugTrackerSU.Web.ViewModels.Articles;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using BugTrackerSU.Common;

    public class ArticlesServicesTests : BaseServicesTests
    {

        [Fact]
        public void ShouldReturnEmbedYoutobeLinkCorrectly()
        {
            ApplicationDbContext db = GetDb();
            var categoryRepository = new EfDeletableEntityRepository<Category>(db);
            var articleRepository = new EfDeletableEntityRepository<Article>(db);
            var service = new ArticleService(articleRepository,categoryRepository);

            string normalYoutubeLink = "https://www.youtube.com/watch?v=sniM-3K7_zQ";

            string expectedEmedLink = "https://www.youtube.com/embed/sniM-3K7_zQ";

            string actualEmedLink = service.GetEmbedYouTubeLink(normalYoutubeLink);

            Assert.Equal(expectedEmedLink, actualEmedLink);
        }

        [Fact]
        public async Task ShouldGetAllCategories()
        {
            ApplicationDbContext db = GetDb();
            var categoryRepository = new EfDeletableEntityRepository<Category>(db);
            var articleRepository = new EfDeletableEntityRepository<Article>(db);
            var service = new ArticleService(articleRepository, categoryRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            var category1 = new Category
            {
                Id = 1,
                Name = "test1",
                Description = "hellooo",
                AddedByUserId = user1.Id,
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
            };


            var category2 = new Category
            {
                Id = 2,
                Name = "test12",
                Description = "hellooo",
                AddedByUserId = user1.Id,
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
            };

            var category3 = new Category
            {
                Id = 3,
                Name = "test123",
                Description = "hellooo",
                AddedByUserId = user1.Id,
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
            };
            await db.Categories.AddAsync(category1);
            await db.SaveChangesAsync();
            await db.Categories.AddAsync(category2);
            await db.SaveChangesAsync();
            await db.Categories.AddAsync(category3);
            await db.SaveChangesAsync();

            var categories = await service.GetAllCategories();

            Assert.True(categories.Count == 3);
            Assert.Contains(categories, c => c.Name == "test1");
        }

        [Fact]
        public async Task CreateArticleAsyncShouldCreateArticle()
        {
            ApplicationDbContext db = GetDb();
            var categoryRepository = new EfDeletableEntityRepository<Category>(db);
            var articleRepository = new EfDeletableEntityRepository<Article>(db);
            var service = new ArticleService(articleRepository, categoryRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            var category1 = new Category
            {
                Id = 1,
                Name = "test1",
                Description = "hellooo",
                AddedByUserId = user1.Id,
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
            };

            await db.Categories.AddAsync(category1);
            await db.SaveChangesAsync();

            var articleModel = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test123",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            await service.CreateArticleAsync(articleModel, user1.Id);

            var article = await articleRepository.All().Where(x => x.Name == articleModel.Name).FirstOrDefaultAsync();

            Assert.NotNull(article);
        }

        [Fact]
        public async Task GetArticlesInCategoryShouldWork()
        {
            ApplicationDbContext db = GetDb();
            var categoryRepository = new EfDeletableEntityRepository<Category>(db);
            var articleRepository = new EfDeletableEntityRepository<Article>(db);
            var service = new ArticleService(articleRepository, categoryRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            var category1 = new Category
            {
                Id = 1,
                Name = "test1",
                Description = "hellooo",
                AddedByUserId = user1.Id,
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
            };

            await db.Categories.AddAsync(category1);
            await db.SaveChangesAsync();

            var articleModel = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test123",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            var articleModel2 = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test1234",
                Description = "lambda 123321 test1",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            await service.CreateArticleAsync(articleModel, user1.Id);
            await service.CreateArticleAsync(articleModel2, user1.Id);

            var articles = await service.GetAllArticlesInCategory(category1.Id,1,10);

            Assert.True(articles.Articles.Count == 2);
        }

        [Fact]
        public async Task EditArticleAsyncShouldWorkProperly()
        {
            ApplicationDbContext db = GetDb();
            var categoryRepository = new EfDeletableEntityRepository<Category>(db);
            var articleRepository = new EfDeletableEntityRepository<Article>(db);
            var service = new ArticleService(articleRepository, categoryRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            var category1 = new Category
            {
                Id = 1,
                Name = "test1",
                Description = "hellooo",
                AddedByUserId = user1.Id,
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
            };

            await db.Categories.AddAsync(category1);
            await db.SaveChangesAsync();

            var articleModel = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test123",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            var editArticleModel = new EditArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "EditedName",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            await service.CreateArticleAsync(articleModel, user1.Id);

            var articleId = articleRepository.All().Where(x => x.Name == "test123").FirstOrDefault().Id;

            await service.EditArticleAsync(editArticleModel, articleId, user1.Id, GlobalConstants.AdministratorRoleName);

            var result = articleRepository.All().Where(x => x.Description == "lambda 123321 test").FirstOrDefault();

            Assert.False(articleModel.Name == result.Name);
        }

        [Fact]
        public async Task DeleteArticleAsyncShouldWork()
        {
            ApplicationDbContext db = GetDb();
            var categoryRepository = new EfDeletableEntityRepository<Category>(db);
            var articleRepository = new EfDeletableEntityRepository<Article>(db);
            var service = new ArticleService(articleRepository, categoryRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            var category1 = new Category
            {
                Id = 1,
                Name = "test1",
                Description = "hellooo",
                AddedByUserId = user1.Id,
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
            };

            await db.Categories.AddAsync(category1);
            await db.SaveChangesAsync();

            var articleModel = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test123",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            await service.CreateArticleAsync(articleModel, user1.Id);

            var articleId = articleRepository.All().Where(x => x.Name == "test123").FirstOrDefault().Id;

            await service.DeleteArticleAsync(articleId, user1.Id, GlobalConstants.AdministratorRoleName);

            var article = await articleRepository.All().Where(x => x.Id == articleId).FirstOrDefaultAsync();

            Assert.Null(article);
        }

        [Fact]
        public async Task ArticlesCountByCategoryIdShouldWorkCorrectly()
        {
            ApplicationDbContext db = GetDb();
            var categoryRepository = new EfDeletableEntityRepository<Category>(db);
            var articleRepository = new EfDeletableEntityRepository<Article>(db);
            var service = new ArticleService(articleRepository, categoryRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            var category1 = new Category
            {
                Id = 1,
                Name = "test1",
                Description = "hellooo",
                AddedByUserId = user1.Id,
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
            };

            await db.Categories.AddAsync(category1);
            await db.SaveChangesAsync();

            var articleModel = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test123",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            var articleModel2 = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test123",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            await service.CreateArticleAsync(articleModel, user1.Id);
            await service.CreateArticleAsync(articleModel2, user1.Id);

            var result = await service.ArticlesCountByCategoryId(category1.Id);

            Assert.True(result == 2);
        }

        [Fact]
        public async Task ArticlesCountShouldWorkCorrectly()
        {
            ApplicationDbContext db = GetDb();
            var categoryRepository = new EfDeletableEntityRepository<Category>(db);
            var articleRepository = new EfDeletableEntityRepository<Article>(db);
            var service = new ArticleService(articleRepository, categoryRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            var category1 = new Category
            {
                Id = 1,
                Name = "test1",
                Description = "hellooo",
                AddedByUserId = user1.Id,
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
            };

            await db.Categories.AddAsync(category1);
            await db.SaveChangesAsync();

            var articleModel = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test123",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            var articleModel2 = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test1234",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            var articleModel3 = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test1234",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            await service.CreateArticleAsync(articleModel, user1.Id);
            await service.CreateArticleAsync(articleModel2, user1.Id);
            await service.CreateArticleAsync(articleModel3, user1.Id);

            var result = await service.ArticlesCount();

            Assert.True(result == 3);
        }

        [Fact]
        public async Task GetAllArticlesShoudlWorkCorrectly()
        {
            ApplicationDbContext db = GetDb();
            var categoryRepository = new EfDeletableEntityRepository<Category>(db);
            var articleRepository = new EfDeletableEntityRepository<Article>(db);
            var service = new ArticleService(articleRepository, categoryRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            var category1 = new Category
            {
                Id = 1,
                Name = "test1",
                Description = "hellooo",
                AddedByUserId = user1.Id,
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
            };

            await db.Categories.AddAsync(category1);
            await db.SaveChangesAsync();

            var articleModel = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test123",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            var articleModel2 = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test1234",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            var articleModel3 = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test1234",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            await service.CreateArticleAsync(articleModel, user1.Id);
            await service.CreateArticleAsync(articleModel2, user1.Id);
            await service.CreateArticleAsync(articleModel3, user1.Id);

            var result = await service.GetAllArticles(1,10);

            Assert.Contains(result.Articles, x => x.Name == "test1234");
            Assert.True(result.Articles.Count == 3);
        }

        [Fact]
        public async Task GetArticleByIdShoudlWorkCorrectly()
        {
            ApplicationDbContext db = GetDb();
            var categoryRepository = new EfDeletableEntityRepository<Category>(db);
            var articleRepository = new EfDeletableEntityRepository<Article>(db);
            var service = new ArticleService(articleRepository, categoryRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            var category1 = new Category
            {
                Id = 1,
                Name = "test1",
                Description = "hellooo",
                AddedByUserId = user1.Id,
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
            };

            await db.Categories.AddAsync(category1);
            await db.SaveChangesAsync();

            var articleModel = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test123",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            var articleModel2 = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test1234",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            var articleModel3 = new CreateArticleFormModel
            {
                CategoryId = category1.Id,
                Name = "test12345",
                Description = "lambda 123321 test",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
                VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
            };

            await service.CreateArticleAsync(articleModel, user1.Id);
            await service.CreateArticleAsync(articleModel2, user1.Id);
            await service.CreateArticleAsync(articleModel3, user1.Id);

            var id = articleRepository.All().Where(x => x.Name == "test12345").FirstOrDefault().Id;

            var result = await service.GetArticleById(id);

            Assert.True(result.Name == "test12345");
        }

    }
}
