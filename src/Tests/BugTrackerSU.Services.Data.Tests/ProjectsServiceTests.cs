namespace BugTrackerSU.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Data;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Data.Repositories;
    using BugTrackerSU.Services.Data.MinorTask;
    using BugTrackerSU.Services.Data.Project;
    using BugTrackerSU.Services.Data.Ticket;
    using BugTrackerSU.Web.ViewModels.MinorTasks;
    using BugTrackerSU.Web.ViewModels.Projects;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ProjectsServiceTests : BaseServicesTests
    {
        [Fact]
        public async Task GetAllProjectsShouldWorkCorretly()
        {
            ApplicationDbContext db = GetDb();
            var ticketRepository = new EfDeletableEntityRepository<Ticket>(db);
            var userProjectRepository = new EfDeletableEntityRepository<ApplicationUserProject>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var projectRepository = new EfDeletableEntityRepository<Project>(db);
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(db);
            var ticketService = new TicketService(projectRepository, userRepository, ticketRepository);
            var projectService = new ProjectService(projectRepository, userRepository, userProjectRepository, roleRepository, ticketRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            var project = new Project
            {
                Id = 2,
                Title = "test123",
                Description = "test123321",
                ProjectManagerId = user1.Id,
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            List<ApplicationUserProject> projectUsers = new List<ApplicationUserProject>
            {
                new ApplicationUserProject
                {
                  ProjectId = 2,
                  ApplicationUserId = user1.Id,
                },
            };

            project.ProjectUsers = projectUsers;

            await db.Projects.AddAsync(project);
            await db.SaveChangesAsync();

            var result = await projectService.GetAllProjects();

            Assert.Single(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllUsersProjectsShouldWorkCorretly()
        {
            ApplicationDbContext db = GetDb();
            var ticketRepository = new EfDeletableEntityRepository<Ticket>(db);
            var userProjectRepository = new EfDeletableEntityRepository<ApplicationUserProject>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var projectRepository = new EfDeletableEntityRepository<Project>(db);
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(db);
            var ticketService = new TicketService(projectRepository, userRepository, ticketRepository);
            var projectService = new ProjectService(projectRepository, userRepository, userProjectRepository, roleRepository, ticketRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            var project = new Project
            {
                Id = 2,
                Title = "test123",
                Description = "test123321",
                ProjectManagerId = user1.Id,
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            List<ApplicationUserProject> projectUsers = new List<ApplicationUserProject>
            {
                new ApplicationUserProject
                {
                  ProjectId = 2,
                  ApplicationUserId = user1.Id,
                },
            };

            project.ProjectUsers = projectUsers;

            await db.Projects.AddAsync(project);
            await db.SaveChangesAsync();

            var result = await projectService.GetUserProjectsCount(user1.Id, GlobalConstants.ProjectManagerRoleName);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task CreateProjectShouldWorkCorrectly()
        {
            ApplicationDbContext db = GetDb();
            var ticketRepository = new EfDeletableEntityRepository<Ticket>(db);
            var userProjectRepository = new EfDeletableEntityRepository<ApplicationUserProject>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var projectRepository = new EfDeletableEntityRepository<Project>(db);
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(db);
            var ticketService = new TicketService(projectRepository, userRepository, ticketRepository);
            var projectService = new ProjectService(projectRepository, userRepository, userProjectRepository, roleRepository, ticketRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            var project = new Project
            {
                Id = 2,
                Title = "test123",
                Description = "test123321",
                ProjectManagerId = user1.Id,
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            List<ApplicationUserProject> projectUsers = new List<ApplicationUserProject>
            {
                new ApplicationUserProject
                {
                  ProjectId = 2,
                  ApplicationUserId = user1.Id,
                },
            };

            project.ProjectUsers = projectUsers;

            await db.Projects.AddAsync(project);
            await db.SaveChangesAsync();

            var model = new CreateProjectViewModel
            {
                Title = "123test",
                Description = "123test321123",
                UserIds = new List<string> { user1.Id },
            };

            await projectService.CreateProjectAsync(model, user1.Id);

            var result = projectRepository.All().Where(x => x.Title == model.Title);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetProjectAssignedUserShoudlWorkCorrectly()
        {
            ApplicationDbContext db = GetDb();
            var ticketRepository = new EfDeletableEntityRepository<Ticket>(db);
            var userProjectRepository = new EfDeletableEntityRepository<ApplicationUserProject>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var projectRepository = new EfDeletableEntityRepository<Project>(db);
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(db);
            var ticketService = new TicketService(projectRepository, userRepository, ticketRepository);
            var projectService = new ProjectService(projectRepository, userRepository, userProjectRepository, roleRepository, ticketRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            var project = new Project
            {
                Id = 2,
                Title = "test123",
                Description = "test123321",
                ProjectManagerId = user1.Id,
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            List<ApplicationUserProject> projectUsers = new List<ApplicationUserProject>
            {
                new ApplicationUserProject
                {
                  ProjectId = 2,
                  ApplicationUserId = user1.Id,
                },
            };

            project.ProjectUsers = projectUsers;

            await db.Projects.AddAsync(project);
            await db.SaveChangesAsync();

            var model = new CreateProjectViewModel
            {
                Title = "123test",
                Description = "123test321123",
                UserIds = new List<string> { user1.Id },
            };

            await projectService.CreateProjectAsync(model, user1.Id);

            var projectId = projectRepository.All().Where(x => x.Title == model.Title).FirstOrDefault().Id;

            var result = await projectService.GetProjectAssignedUsers(projectId);
            var result2 = await projectService.GetProjectAssignedDevelopers(projectId);

            Assert.True(result.Count == 1);
            Assert.True(result2.Count == 1);
        }

        [Fact]
        public async Task ChekIfProjectIsValid()
        {
            ApplicationDbContext db = GetDb();
            var ticketRepository = new EfDeletableEntityRepository<Ticket>(db);
            var userProjectRepository = new EfDeletableEntityRepository<ApplicationUserProject>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var projectRepository = new EfDeletableEntityRepository<Project>(db);
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(db);
            var ticketService = new TicketService(projectRepository, userRepository, ticketRepository);
            var projectService = new ProjectService(projectRepository, userRepository, userProjectRepository, roleRepository, ticketRepository);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            var project = new Project
            {
                Id = 2,
                Title = "test123",
                Description = "test123321",
                ProjectManagerId = user1.Id,
            };

            await db.Users.AddAsync(user1);
            await db.SaveChangesAsync();

            List<ApplicationUserProject> projectUsers = new List<ApplicationUserProject>
            {
                new ApplicationUserProject
                {
                  ProjectId = 2,
                  ApplicationUserId = user1.Id,
                },
            };

            project.ProjectUsers = projectUsers;

            await db.Projects.AddAsync(project);
            await db.SaveChangesAsync();

            var model = new CreateProjectViewModel
            {
                Title = "123test",
                Description = "123test321123",
                UserIds = new List<string> { user1.Id },
            };

            await projectService.CreateProjectAsync(model, user1.Id);

            var projectId = projectRepository.All().Where(x => x.Title == model.Title).FirstOrDefault().Id;

            var result = await projectService.ChekIfProjectIsValid(projectId);

            Assert.True(result);
        }
    }
}
