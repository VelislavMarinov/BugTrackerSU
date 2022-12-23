namespace BugTrackerSU.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Data.Models;

    public class ArticlesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Articles.Any())
            {
                return;
            }

            var articles = new List<Article>
            {
                new Article
                {
                    Name = "Eliminate distractions",
                    Description = @"Keep Your Vision and Goals in Mind, Clarify Your Day Before You Start, Reduce the Chaos of Your Day",
                    AddedByUser = dbContext.Users.Where(x => x.UserName == "Radi").FirstOrDefault(),
                    Category = dbContext.Categories.Where(x => x.Name == "Focus").FirstOrDefault(),
                    VideoUrl = "https://www.youtube.com/embed/KZGVgz9b2fw",
                    ImageUrl = "https://smallbusinessify.com/wp-content/uploads/2021/04/Proven-Strategies-for-Overcoming-Distractions-1024x751.jpg",
                },

                new Article
                {
                    Name = "Reduce multitasking.",
                    Description = @"Multitasking reduces your efficiency and performance because your brain can only focus on one thing at a time. When you try to do two things at once, your brain lacks the capacity to perform both tasks successfully.",
                    AddedByUser = dbContext.Users.Where(x => x.UserName == "User").FirstOrDefault(),
                    Category = dbContext.Categories.Where(x => x.Name == "Focus").FirstOrDefault(),
                    VideoUrl = "https://www.youtube.com/embed/tMiyzuO1qMs",
                    ImageUrl = "https://smallbusinessify.com/wp-content/uploads/2021/04/Proven-Strategies-for-Overcoming-Distractions-1024x751.jpg",
                },

                new Article
                {
                    Name = "Yoga",
                    Description = @"Yoga is a physical, mental and spiritual practice that originated in ancient India. First codified by the sage Patanjali in his Yoga Sutras around 400 C.E, the practice was in fact handed down from teacher to student long before this text arose. Traditionally, this was a one-to-one transmission, but since yoga became popular in the West in the 20th century, group classes have become the norm.",
                    AddedByUser = dbContext.Users.Where(x => x.UserName == "Nikola").FirstOrDefault(),
                    Category = dbContext.Categories.Where(x => x.Name == "Training").FirstOrDefault(),
                    VideoUrl = "https://www.youtube.com/embed/g9h6anH7n0U",
                    ImageUrl = "https://play-lh.googleusercontent.com/vOpMqY5S3wzVi6-NGZs_gKgzbCX__iGZUdFyCtLnKYlunr-Sbeq43l5Ras14WnwUb6NG",
                },

                new Article
                {
                    Name = "Gym training",
                    Description = @"Such exercises also improve glucose metabolism, enhance maintenance of healthy body weight, and help improve cardiovascular risk factors such as blood pressure,” she said. “All these factors lead to lower risks of cardiovascular disease, cancer, and diabetes, which lowers mortality risk",
                    AddedByUser = dbContext.Users.Where(x => x.UserName == "Kaloqn").FirstOrDefault(),
                    Category = dbContext.Categories.Where(x => x.Name == "Training").FirstOrDefault(),
                    VideoUrl = "https://www.youtube.com/embed/eMjyvIQbn9M",
                    ImageUrl = "https://octaneairsrc.com/pqkrkdpyafyo57c2/quizimg/9b18e753-3901-4f31-9377-bbc54f66b59d",
                },

                new Article
                {
                    Name = "Pomodoro",
                    Description = @"Keep Your Vision and Goals in Mind, Clarify Your Day Before You Start, Reduce the Chaos of Your Day",
                    AddedByUser = dbContext.Users.Where(x => x.UserName == "Pesho").FirstOrDefault(),
                    Category = dbContext.Categories.Where(x => x.Name == "How to study").FirstOrDefault(),
                    VideoUrl = "https://www.youtube.com/embed/1pADI_eZ_-U",
                    ImageUrl = "https://web-static.wrike.com/cdn-cgi/image/width=900,format=auto/blog/content/uploads/2021/11/iStock-1302208532.jpg?av=15112cfb5bfa14185f0d98bd9d046e40",
                },

                new Article
                {
                    Name = "Distributed practice",
                    Description = @"Keep Your Vision and Goals in Mind, Clarify Your Day Before You Start, Reduce the Chaos of Your Day",
                    AddedByUser = dbContext.Users.Where(x => x.UserName == "Ivan").FirstOrDefault(),
                    Category = dbContext.Categories.Where(x => x.Name == "How to study").FirstOrDefault(),
                    VideoUrl = "https://www.youtube.com/embed/sniM-3K7_zQ",
                    ImageUrl = "https://i.ytimg.com/vi/sniM-3K7_zQ/maxresdefault.jpg",
                },
            };

            foreach (var article in articles)
            {
                await dbContext.Articles.AddAsync(new Article
                {
                    Name = article.Name,
                    Description = article.Description,
                    AddedByUser = article.AddedByUser,
                    AddedByUserId = article.AddedByUser.Id,
                    Category = article.Category,
                    VideoUrl = article.VideoUrl,
                    ImageUrl = article.ImageUrl,

                });
            }
        }
    }
}
