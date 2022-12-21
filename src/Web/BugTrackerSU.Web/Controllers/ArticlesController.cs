﻿namespace BugTrackerSU.Web.Controllers
{
    using BugTrackerSu.Web;
    using BugTrackerSU.Services.Data.Article;
    using BugTrackerSU.Web.ViewModels.Articles;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ArticlesController : BaseController
    {
        private readonly IArticleService articleService;

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

            var userId = this.User.GetId();

            await this.articleService.CreateArticleAsync(model, userId);

            return this.RedirectToAction("All", "Articles");
        }

        [HttpGet]
        public IActionResult All(int id = 1)
        {
            var itemsPerPage = 5;

            var model = this.articleService.GetAllArticles(id, itemsPerPage);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult ByCategory(int categoryId, int id = 1)
        {
            var itemsPerPage = 5;

            var model = this.articleService.GetAllArticlesInCategory(categoryId, id, itemsPerPage);

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new EditArticleFormModel();
            model.Categories = this.articleService.GetAllCategories();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditArticleFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = this.articleService.GetAllCategories();
                return this.View(model);
            }

            await this.articleService.EditArticleAsync(model, id);

            return this.RedirectToAction("All", "Articles");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.articleService.DeleteArticleAsync(id);
            this.TempData["Message"] = "Article deleted successfully.";
            return this.RedirectToAction("All", "Articles");
        }

    }
}