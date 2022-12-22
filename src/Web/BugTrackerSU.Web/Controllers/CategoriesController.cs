namespace BugTrackerSU.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Services.Data.Category;
    using BugTrackerSU.Services.Data.User;
    using BugTrackerSu.Web;
    using BugTrackerSU.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        private readonly ICategoryService categoryService;

        private readonly IUserService userService;

        private readonly int itemsPerPage = PagingConstants.CategoriesPagingItemsPerPage;

        public CategoriesController(
            ICategoryService categoryService,
            IUserService userService)
        {
            this.categoryService = categoryService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateCategoryFormModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var modelCreate = new CreateCategoryFormModel();

                return this.View(modelCreate);
            }

            try
            {
                var userId = this.User.GetId();

                await this.categoryService.CreateCategoryAsync(model, userId);

                return this.RedirectToAction("All", "Categories");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult All(int id = 1)
        {
            var model = this.categoryService.GetAllCategories(id, this.itemsPerPage);

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var model = new EditCategoryFormModel();

                return this.View(model);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCategoryFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.categoryService.EditCategoryAsync(model, id);

                return this.RedirectToAction("All", "Categories");
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
                var userId = this.User.GetId();
                var userRole = this.userService.GetUserRole(this.User);

                await this.categoryService.DeleteCategoryAsync(id, userId, userRole);
                this.TempData["Message"] = "Category deleted successfully.";
                return this.RedirectToAction("All", "Categories");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

    }
}
