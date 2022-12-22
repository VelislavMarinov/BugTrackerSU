namespace BugTrackerSU.Web.Controllers
{
    using BugTrackerSu.Web;
    using BugTrackerSU.Services.Data.Article;
    using BugTrackerSU.Web.ViewModels.Articles;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using System;

    public class ArticlesController : BaseController
    {
        private readonly IArticleService articleService;

        private readonly int itemsPerPage = PagingConstants.ArticlesPagingItemsPerPage;

        public ArticlesController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateArticleFormModel();
            model.Categories = this.articleService.GetAllCategories();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = this.articleService.GetAllCategories();
                return this.View(model);
            }

            try
            {
                var userId = this.User.GetId();

                await this.articleService.CreateArticleAsync(model, userId);

                return this.RedirectToAction("All", "Articles");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult All(int id = 1)
        {
            var model = this.articleService.GetAllArticles(id, this.itemsPerPage);

            return this.View(model);
        }

        public IActionResult ByCategory(int categoryId, int id = 1)
        {
            var model = this.articleService.GetAllArticlesInCategory(categoryId, id, this.itemsPerPage);

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var model = new EditArticleFormModel();
                model.Categories = this.articleService.GetAllCategories();

                return this.View(model);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditArticleFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = this.articleService.GetAllCategories();
                return this.View(model);
            }

            try
            {
                await this.articleService.EditArticleAsync(model, id);

                return this.RedirectToAction("All", "Articles");
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.articleService.DeleteArticleAsync(id);
                this.TempData["Message"] = "Article deleted successfully.";
                return this.RedirectToAction("All", "Articles");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Article(int id)
        {
            var article = this.articleService.GetArticleById(id);
            return this.View(article);
        }

    }
}
