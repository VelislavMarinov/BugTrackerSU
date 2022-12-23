namespace BugTrackerSU.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using BugTrackerSU.Common;
    using BugTrackerSU.Data;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Data.Repositories;
    using BugTrackerSU.Services.Data.Category;
    using BugTrackerSU.Web.ViewModels.Categories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CategoryServiceTests : BaseServicesTests
    {

        [Fact]
        public async Task ShouldReturnCount()
        {
            ApplicationDbContext db = GetDb();
            var categoryRepository = new EfDeletableEntityRepository<Category>(db);
            var service = new CategoryService(categoryRepository);

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

            Assert.True(await service.CategoriesCount() == 3);
        }

        [Fact]
        public async Task ShouldCreateCategory()
        {
            ApplicationDbContext db = GetDb();
            var categoryRepository = new EfDeletableEntityRepository<Category>(db);
            var service = new CategoryService(categoryRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            var model = new CreateCategoryFormModel
            {
                Name = "test1",
                Description = "hellooo",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
            };

            await service.CreateCategoryAsync(model, user1.Id);

            var category = await db.Categories.Where(x => x.Name == "test1").FirstOrDefaultAsync();

            Assert.NotNull(category);
        }

        [Fact]
        public async Task ShouldEditCategoryProperly()
        {
            ApplicationDbContext db = GetDb();
            var categoryRepository = new EfDeletableEntityRepository<Category>(db);
            var service = new CategoryService(categoryRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            var model = new CreateCategoryFormModel
            {
                Name = "test1",
                Description = "hellooo",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
            };

            await service.CreateCategoryAsync(model, user1.Id);

            var categoryId = db.Categories.Where(x => x.Name == "test1").FirstOrDefault().Id;

            var editModel = new EditCategoryFormModel()
            {
                Name = "test2",
                Description = "hellooo",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
            };

            await service.EditCategoryAsync(editModel, categoryId, user1.Id, GlobalConstants.AdministratorRoleName);

            var category = db.Categories.Where(x => x.Id == categoryId).FirstOrDefault();

            Assert.NotEqual(category.Name, model.Name);
        }

        [Fact]
        public async Task ShouldDeleteProperly()
        {
            ApplicationDbContext db = GetDb();
            var categoryRepository = new EfDeletableEntityRepository<Category>(db);
            var service = new CategoryService(categoryRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            var model = new CreateCategoryFormModel
            {
                Name = "test1",
                Description = "hellooo",
                ImageUrl = "https://sienaconstruction.com/wp-content/uploads/2017/05/test-image.jpg",
            };

            await service.CreateCategoryAsync(model, user1.Id);

            var categoryId = db.Categories.Where(x => x.Name == "test1").FirstOrDefault().Id;

            await service.DeleteCategoryAsync(categoryId, user1.Id, GlobalConstants.AdministratorRoleName);

            var category = db.Categories.Where(x => x.Id == categoryId).FirstOrDefault();

            Assert.Null(category);
        }

        [Fact]
        public async Task ShouldGetAllCategories()
        {
            ApplicationDbContext db = GetDb();
            var categoryRepository = new EfDeletableEntityRepository<Category>(db);
            var service = new CategoryService(categoryRepository);

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

            var categories = await service.GetAllCategories(1, 10);

            Assert.True(categories.Categories.Count() == 3);
            Assert.Contains(categories.Categories, c => c.Name == "test1");
        }
    }
}
