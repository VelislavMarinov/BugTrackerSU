namespace BugTrackerSU.Data.Seeding
{
    using BugTrackerSU.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProjectsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Projects.Any(x => x.Title == "BugTrackerSU"))
            {
                var userProject1 = new List<ApplicationUser>();
                userProject1.Add(dbContext.Users.Where(x => x.UserName == "User").FirstOrDefault());
                userProject1.Add(dbContext.Users.Where(x => x.UserName == "Ivan").FirstOrDefault());

                var project1 = new Project
                {
                    Title = "BugTrackerSU",
                    Description = "Making the best BugTracker overtime.",
                    ProjectManagerId = dbContext.Users.Where(x => x.UserName == "Pesho").FirstOrDefault().Id,
                };
                foreach (var user in userProject1)
                {
                    var userProject = new ApplicationUserProject
                    {
                        Project = project1,
                        ProjectId = project1.Id,
                        ApplicationUser = user,
                        ApplicationUserId = user.Id,
                    };
                    project1.ProjectUsers.Add(userProject);
                }

                await dbContext.Projects.AddAsync(project1);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Projects.Any(x => x.Title == "TheAncientMerch"))
            {
                var userProject2 = new List<ApplicationUser>();
                userProject2.Add(dbContext.Users.Where(x => x.UserName == "Kaloqn").FirstOrDefault());
                userProject2.Add(dbContext.Users.Where(x => x.UserName == "Manol").FirstOrDefault());

                var project2 = new Project
                {
                    Title = "TheAncientMerch",
                    Description = "Developing a website for studying and selling sculptures..",
                    ProjectManagerId = dbContext.Users.Where(x => x.UserName == "Nikola").FirstOrDefault().Id,
                };
                foreach (var user in userProject2)
                {
                    var userProject = new ApplicationUserProject
                    {
                        Project = project2,
                        ProjectId = project2.Id,
                        ApplicationUser = user,
                        ApplicationUserId = user.Id,
                    };
                    project2.ProjectUsers.Add(userProject);
                }

                await dbContext.Projects.AddAsync(project2);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Projects.Any(x => x.Title == "LearnPlanProfit"))
            {
                var userProject3 = new List<ApplicationUser>();
                userProject3.Add(dbContext.Users.Where(x => x.UserName == "User").FirstOrDefault());
                userProject3.Add(dbContext.Users.Where(x => x.UserName == "Manol").FirstOrDefault());

                var project3 = new Project
                {
                    Title = "LearnPlanProfit",
                    Description = "Trading platform, with courses to study for.",
                    ProjectManagerId = dbContext.Users.Where(x => x.UserName == "Pesho").FirstOrDefault().Id,
                };
                foreach (var user in userProject3)
                {
                    var userProject = new ApplicationUserProject
                    {
                        Project = project3,
                        ProjectId = project3.Id,
                        ApplicationUser = user,
                        ApplicationUserId = user.Id,
                    };
                    project3.ProjectUsers.Add(userProject);
                }

                await dbContext.Projects.AddAsync(project3);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Projects.Any(x => x.Title == "MonsterCat"))
            {
                var userProject4 = new List<ApplicationUser>();
                userProject4.Add(dbContext.Users.Where(x => x.UserName == "Ivan").FirstOrDefault());
                userProject4.Add(dbContext.Users.Where(x => x.UserName == "Kaloqn").FirstOrDefault());

                var project4 = new Project
                {
                    Title = "MonsterCat",
                    Description = "Web application for electronic devacies.",
                    ProjectManagerId = dbContext.Users.Where(x => x.UserName == "Nikola").FirstOrDefault().Id,
                };
                foreach (var user in userProject4)
                {
                    var userProject = new ApplicationUserProject
                    {
                        Project = project4,
                        ProjectId = project4.Id,
                        ApplicationUser = user,
                        ApplicationUserId = user.Id,
                    };
                    project4.ProjectUsers.Add(userProject);
                }

                await dbContext.Projects.AddAsync(project4);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Projects.Any(x => x.Title == "Im Still Standing"))
            {
                var userProject5 = new List<ApplicationUser>();
                userProject5.Add(dbContext.Users.Where(x => x.UserName == "Ivan").FirstOrDefault());
                userProject5.Add(dbContext.Users.Where(x => x.UserName == "Qvor").FirstOrDefault());

                var project5 = new Project
                {
                    Title = "Im Still Standing",
                    Description = "This song i keeping me alive currently.",
                    ProjectManagerId = dbContext.Users.Where(x => x.UserName == "Pesho").FirstOrDefault().Id,
                };
                foreach (var user in userProject5)
                {
                    var userProject = new ApplicationUserProject
                    {
                        Project = project5,
                        ProjectId = project5.Id,
                        ApplicationUser = user,
                        ApplicationUserId = user.Id,
                    };
                    project5.ProjectUsers.Add(userProject);
                }

                await dbContext.Projects.AddAsync(project5);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
