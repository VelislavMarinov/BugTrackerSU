namespace BugTrackerSU.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Focus",
                    Description = @"Train your mind & your body will follow.",
                    AddedByUser = dbContext.Users.Where(x => x.UserName == "Pesho").FirstOrDefault(),
                    ImageUrl = @"https://socialanxietyinstitute.org/sites/default/files/Focus.jpg",
                },

                new Category
                {
                    Name = "Training",
                    Description = @"Strong body and strong mind will help you develop two times faster.",
                    AddedByUser = dbContext.Users.Where(x => x.UserName == "Radi").FirstOrDefault(),
                    ImageUrl = @"https://images.everydayhealth.com/images/healthy-living/fitness/all-about-yoga-mega-722x406.jpg",
                },

                new Category
                {
                    Name = "How to study",
                    Description = @"Techniques to make your bug tracking and learning easy.",
                    AddedByUser = dbContext.Users.Where(x => x.UserName == "Qvor").FirstOrDefault(),
                    ImageUrl = @"https://previews.123rf.com/images/dizanna/dizanna1606/dizanna160600706/57871891-techniques-bulb-word-cloud-business-concept.jpg",
                },
            };

            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(new Category
                {
                    Name = category.Name,
                    Description = category.Description,
                    AddedByUser = category.AddedByUser,
                    AddedByUserId = category.AddedByUser.Id,
                    ImageUrl = category.ImageUrl,
                });
            }
        }
    }
}
