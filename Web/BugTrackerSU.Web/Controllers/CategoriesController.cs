namespace BugTrackerSU.Web.Controllers
{
    using System.Threading.Tasks;
    using BugTrackerSu.Web;
    using BugTrackerSU.Services.Data.Category;
    using BugTrackerSU.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
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

            var userId = this.User.GetId();

            await this.categoryService.Create(model, userId);

            return this.Redirect("/");
        }

    }
}
