namespace BugTrackerSU.Services.Data.Article
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.Articles;
    using Microsoft.EntityFrameworkCore;

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

        public string GetEmbedYouTubeLink(string rawLink)
        {
            var result = string.Empty;
            if (rawLink.Contains("watch"))
            {
                var source1 = rawLink.Split("watch")[0];
                var source2 = rawLink.Split("=")[1];
                var source3 = source2.Split("&")[0];
                result = source1 + "embed/" + source3;
            }
            else if (rawLink.Contains("youtu.be"))
            {
                var source1 = rawLink.Split("youtu.be/")[0];
                var source2 = rawLink.Split("youtu.be/")[1];
                result = source1 + "www.youtube.com/embed/" + source2;
            }

            return result;
        }

        public async Task<int> ArticlesCountByCategoryId(int categoryId) => await this.articleRepository.All().Where(x => x.CategoryId == categoryId).CountAsync();

        public async Task CreateArticleAsync(CreateArticleFormModel model, string userId)
        {
            var article = new Article
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                VideoUrl = this.GetEmbedYouTubeLink(model.VideoUrl),
                AddedByUserId = userId,
                CategoryId = model.CategoryId,
            };

            await this.articleRepository.AddAsync(article);
            await this.articleRepository.SaveChangesAsync();
        }

        public async Task DeleteArticleAsync(int articleId, string userId, string userRole)
        {
            if (userRole == GlobalConstants.AdministratorRoleName)
            {
                var adminArticle = this.articleRepository
               .All()
               .Where(x => x.Id == articleId)
               .FirstOrDefault();

                if (adminArticle == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    this.articleRepository.Delete(adminArticle);
                    await this.articleRepository.SaveChangesAsync();
                }
            }
            else
            {
                var userArticle = this.articleRepository
               .All()
               .Where(x => x.Id == articleId && x.AddedByUserId == userId)
               .FirstOrDefault();

                if (userArticle == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    this.articleRepository.Delete(userArticle);
                    await this.articleRepository.SaveChangesAsync();
                }
            }
        }

        public async Task EditArticleAsync(EditArticleFormModel model, int articleId, string userId, string roleName)
        {
            if (roleName == GlobalConstants.AdministratorRoleName)
            {
                var adminArticle = this.articleRepository
               .All()
               .Where(x => x.Id == articleId)
               .FirstOrDefault();

                if (adminArticle == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    adminArticle.Name = model.Name;
                    adminArticle.Description = model.Description;
                    adminArticle.ImageUrl = model.ImageUrl;
                    adminArticle.VideoUrl = this.GetEmbedYouTubeLink(model.VideoUrl);
                    adminArticle.CategoryId = model.CategoryId;

                    this.articleRepository.Update(adminArticle);
                    await this.articleRepository.SaveChangesAsync();
                }
            }
            else
            {
               var userArticle = this.articleRepository
              .All()
              .Where(x => x.Id == articleId && x.AddedByUserId == userId)
              .FirstOrDefault();

               if (userArticle == null)
               {
                    throw new NullReferenceException();
               }
               else
               {
                    userArticle.Name = model.Name;
                    userArticle.Description = model.Description;
                    userArticle.ImageUrl = model.ImageUrl;
                    userArticle.VideoUrl = this.GetEmbedYouTubeLink(model.VideoUrl);
                    userArticle.CategoryId = model.CategoryId;

                    this.articleRepository.Update(userArticle);
                    await this.articleRepository.SaveChangesAsync();
               }
            }
        }

        public async Task<AllArticlesViewModel> GetAllArticlesInCategory(int categoryId, int pageNumber, int itemsPerPage)
        {
            var articles = await this.articleRepository
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
                .ToListAsync();

            var model = new AllArticlesViewModel
            {
                Categories = await this.GetAllCategories(),
                ItemsCount = await this.ArticlesCountByCategoryId(categoryId),
                PageNumber = pageNumber,
                ItemsPerPage = itemsPerPage,
                Articles = articles,
                CategoryId = categoryId,
            };

            return model;
        }

        public async Task<AllArticlesViewModel> GetAllArticles(int pageNumber, int itemsPerPage)
        {
            var articles = await this.articleRepository
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
                .ToListAsync();

            var model = new AllArticlesViewModel
            {
                Categories = await this.GetAllCategories(),
                ItemsCount = await this.ArticlesCount(),
                PageNumber = pageNumber,
                ItemsPerPage = itemsPerPage,
                Articles = articles,
            };

            return model;
        }

        public async Task<ICollection<ArticleCategoryViewModel>> GetAllCategories()
        {
            var categories = await this.categoryRepository
                .All()
                .Select(x => new ArticleCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            return categories;
        }

        public async Task<int> ArticlesCount() => await this.articleRepository.All().CountAsync();

        public async Task<ArticleViewModel> GetArticleById(int articleId)
        {
            var model = await this.articleRepository
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
                .FirstOrDefaultAsync();

            return model;
        }
    }
}
