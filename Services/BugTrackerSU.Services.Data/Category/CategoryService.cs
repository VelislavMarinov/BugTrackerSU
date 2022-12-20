namespace BugTrackerSU.Services.Data.Category
{
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.Categories;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository)
        {
           this.categoryRepository = categoryRepository;
        }

        public int CategoriesCount() => this.categoryRepository.All().Count();

        public async Task Create(CreateCategoryFormModel model, string userId)
        {
            var category = new Category
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public AllCategoriesViewModel GetAllCategories(int pageNumber, int itemsPerPage)
        {
            var categories = this.categoryRepository
                .All()
                .Select(x => new CategoryViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    AddedBy = x.AddedByUser.UserName,
                })
                .ToList();

            var model = new AllCategoriesViewModel
            {
                Categories = categories,
                PageNumber = pageNumber,
                ItemsPerPage = itemsPerPage,
                ItemsCount = this.CategoriesCount(),
            };

            return model;
        }
    }
}
