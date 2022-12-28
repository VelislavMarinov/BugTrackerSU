namespace BugTrackerSU.Services.Data.Category
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.Categories;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository)
        {
           this.categoryRepository = categoryRepository;
        }

        public async Task<int> CategoriesCount() => await this.categoryRepository.All().CountAsync();

        public async Task CreateCategoryAsync(CreateCategoryFormModel model, string userId)
        {
            var category = new Category
            {
                AddedByUserId = userId,
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId, string userId, string userRole)
        {
            if (userRole == GlobalConstants.AdministratorRoleName)
            {
                var adminCategory = this.categoryRepository
                .All()
                .Where(x => x.Id == categoryId)
                .FirstOrDefault();

                if (adminCategory == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    this.categoryRepository.Delete(adminCategory);
                    await this.categoryRepository.SaveChangesAsync();
                }
            }
            else
            {
                var userCategory = this.categoryRepository
               .All()
               .Where(x => x.Id == categoryId && x.AddedByUserId == userId)
               .FirstOrDefault();

                if (userCategory == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    this.categoryRepository.Delete(userCategory);
                    await this.categoryRepository.SaveChangesAsync();
                }
            }
        }

        public async Task EditCategoryAsync(EditCategoryFormModel model, int categoryId, string userId, string userRole)
        {
            if (userRole == GlobalConstants.AdministratorRoleName)
            {
                var adminCategory = this.categoryRepository
               .All()
               .Where(x => x.Id == categoryId)
               .FirstOrDefault();

                if (adminCategory == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    adminCategory.Name = model.Name;
                    adminCategory.Description = model.Description;
                    adminCategory.ImageUrl = model.ImageUrl;

                    this.categoryRepository.Update(adminCategory);
                    await this.categoryRepository.SaveChangesAsync();
                }
            }
            else
            {
                var userCategory = this.categoryRepository
               .All()
               .Where(x => x.Id == categoryId && x.AddedByUserId == userId)
               .FirstOrDefault();

                if (userCategory == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    userCategory.Name = model.Name;
                    userCategory.Description = model.Description;
                    userCategory.ImageUrl = model.ImageUrl;

                    this.categoryRepository.Update(userCategory);
                    await this.categoryRepository.SaveChangesAsync();
                }
            }
        }

        public async Task<AllCategoriesViewModel> GetAllCategories(int pageNumber, int itemsPerPage)
        {
            var categories = await this.categoryRepository
                .All()
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    AddedBy = x.AddedByUser.UserName,
                })
                .ToListAsync();

            var model = new AllCategoriesViewModel
            {
                Categories = categories,
                PageNumber = pageNumber,
                ItemsPerPage = itemsPerPage,
                ItemsCount = await this.CategoriesCount(),
            };

            return model;
        }
    }
}
