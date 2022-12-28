namespace BugTrackerSU.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BugTrackerSU.Common;
    using BugTrackerSU.Data;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Data.Repositories;
    using BugTrackerSU.Services.Data.MinorTask;
    using BugTrackerSU.Services.Data.Ticket;
    using BugTrackerSU.Web.ViewModels.MinorTasks;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class MinorTasksServiceTests : BaseServicesTests
    {
        [Fact]
        public async Task CreateMinorTaskAsyncShouldWorkCorrectly()
        {
            ApplicationDbContext db = GetDb();
            var ticketRepository = new EfDeletableEntityRepository<Ticket>(db);
            var minorTaskRepository = new EfDeletableEntityRepository<MinorTask>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var projectRepository = new EfDeletableEntityRepository<Project>(db);
            var ticketService = new TicketService(projectRepository, userRepository, ticketRepository);
            var minorTaskService = new MinorTaskService(minorTaskRepository, ticketRepository, ticketService);

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


            var ticket = new Ticket
            {
                Title = "Missing information",
                Description = "Try and search all deleted infomation fro user #22231",
                AssignedDeveloperId = user1.Id,
                TicketSubmitterId = user1.Id,
                ProjectId = project.Id,
                TicketType = "OtherComments",
                Priority = "Low",
                Status = "InProgress",
            };


            await db.Tickets.AddAsync(ticket);
            await db.SaveChangesAsync();

            var minorTask = new CreateMinorTaskFormModel
            {
                Title = "test123",
                Content = "Content123",
                TicketId = ticket.Id,
                TaskType = BugTrackerSU.Data.Models.Enums.TaskType.Task,
            };

            await minorTaskService.CreateMinorTaskAsync(minorTask, user1.Id);

            var result = await minorTaskRepository.All().Where(x => x.Title == "test123").FirstOrDefaultAsync();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ChekIfUserIsAthorizedToSeeOrCreateTaskShouldWorkCorrectly()
        {
            ApplicationDbContext db = GetDb();
            var ticketRepository = new EfDeletableEntityRepository<Ticket>(db);
            var minorTaskRepository = new EfDeletableEntityRepository<MinorTask>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var projectRepository = new EfDeletableEntityRepository<Project>(db);
            var ticketService = new TicketService(projectRepository, userRepository, ticketRepository);
            var minorTaskService = new MinorTaskService(minorTaskRepository, ticketRepository, ticketService);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            var user2 = new ApplicationUser
            {
                Id = "y1234",
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
            await db.Users.AddAsync(user2);
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


            var ticket = new Ticket
            {
                Title = "Missing information",
                Description = "Try and search all deleted infomation fro user #22231",
                AssignedDeveloperId = user1.Id,
                TicketSubmitterId = user1.Id,
                ProjectId = project.Id,
                TicketType = "OtherComments",
                Priority = "Low",
                Status = "InProgress",
            };


            await db.Tickets.AddAsync(ticket);
            await db.SaveChangesAsync();

            var minorTask = new CreateMinorTaskFormModel
            {
                Title = "test123",
                Content = "Content123",
                TicketId = ticket.Id,
                TaskType = BugTrackerSU.Data.Models.Enums.TaskType.Task,
            };

            await minorTaskService.CreateMinorTaskAsync(minorTask, user1.Id);

            bool resultExpectedTrue = await minorTaskService.ChekIfUserIsAuthorizedToCreateOrSeeTask(ticket.Id, user1.Id, GlobalConstants.ProjectManagerRoleName);

            bool resultExpectedFalse = await minorTaskService.ChekIfUserIsAuthorizedToCreateOrSeeTask(ticket.Id, user2.Id, GlobalConstants.ProjectManagerRoleName);

            Assert.True(resultExpectedTrue);
            Assert.False(resultExpectedFalse);
        }

        [Fact]
        public async Task GetTicketTasksByTicketIdShouldWorkCorrectly()
        {
            ApplicationDbContext db = GetDb();
            var ticketRepository = new EfDeletableEntityRepository<Ticket>(db);
            var minorTaskRepository = new EfDeletableEntityRepository<MinorTask>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var projectRepository = new EfDeletableEntityRepository<Project>(db);
            var ticketService = new TicketService(projectRepository, userRepository, ticketRepository);
            var minorTaskService = new MinorTaskService(minorTaskRepository, ticketRepository, ticketService);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            var user2 = new ApplicationUser
            {
                Id = "y1234",
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
            await db.Users.AddAsync(user2);
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


            var ticket = new Ticket
            {
                Title = "Missing information",
                Description = "Try and search all deleted infomation fro user #22231",
                AssignedDeveloperId = user1.Id,
                TicketSubmitterId = user1.Id,
                ProjectId = project.Id,
                TicketType = "OtherComments",
                Priority = "Low",
                Status = "InProgress",
            };


            await db.Tickets.AddAsync(ticket);
            await db.SaveChangesAsync();

            var minorTask = new CreateMinorTaskFormModel
            {
                Title = "test123",
                Content = "Content123",
                TicketId = ticket.Id,
                TaskType = BugTrackerSU.Data.Models.Enums.TaskType.Task,
            };

            var minorTask2 = new CreateMinorTaskFormModel
            {
                Title = "test1234",
                Content = "Content1234",
                TicketId = ticket.Id,
                TaskType = BugTrackerSU.Data.Models.Enums.TaskType.Task,
            };

            await minorTaskService.CreateMinorTaskAsync(minorTask, user1.Id);
            await minorTaskService.CreateMinorTaskAsync(minorTask2, user1.Id);

            var tasks = await minorTaskService.GetTicketTasksById(ticket.Id, 1, 10);
            var count = await minorTaskService.GetTicketTasksCount(ticket.Id);

            Assert.True(tasks.Tasks.Count == 2);
            Assert.False(tasks.Tasks.Count != count);
            Assert.True(count == 2);
        }

        [Fact]
        public async Task StartTaskAndFinishTaskShouldWorkCorrectly()
        {
            ApplicationDbContext db = GetDb();
            var ticketRepository = new EfDeletableEntityRepository<Ticket>(db);
            var minorTaskRepository = new EfDeletableEntityRepository<MinorTask>(db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(db);
            var projectRepository = new EfDeletableEntityRepository<Project>(db);
            var ticketService = new TicketService(projectRepository, userRepository, ticketRepository);
            var minorTaskService = new MinorTaskService(minorTaskRepository, ticketRepository, ticketService);

            var user1 = new ApplicationUser
            {
                Id = "y123",
                UserName = "Gosho",
                Email = "gosho@goshov.bb",
            };

            var user2 = new ApplicationUser
            {
                Id = "y1234",
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
            await db.Users.AddAsync(user2);
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

            var ticket = new Ticket
            {
                Title = "Missing information",
                Description = "Try and search all deleted infomation fro user #22231",
                AssignedDeveloperId = user1.Id,
                TicketSubmitterId = user1.Id,
                ProjectId = project.Id,
                TicketType = "OtherComments",
                Priority = "Low",
                Status = "InProgress",
            };

            await db.Tickets.AddAsync(ticket);
            await db.SaveChangesAsync();

            var minorTask = new CreateMinorTaskFormModel
            {
                Title = "test123",
                Content = "Content123",
                TicketId = ticket.Id,
                TaskType = BugTrackerSU.Data.Models.Enums.TaskType.Task,
            };

            var minorTask2 = new CreateMinorTaskFormModel
            {
                Title = "test1234",
                Content = "Content1234",
                TicketId = ticket.Id,
                TaskType = BugTrackerSU.Data.Models.Enums.TaskType.Task,
            };

            await minorTaskService.CreateMinorTaskAsync(minorTask, user1.Id);
            await minorTaskService.CreateMinorTaskAsync(minorTask2, user1.Id);

            var tasks = await minorTaskService.GetTicketTasksById(ticket.Id, 1, 10);

            for (int i = 0; i < tasks.Tasks.Count; i++)
            {
               await minorTaskService.StartTask(tasks.Tasks[i].TaskId);
               await minorTaskService.FinishTask(tasks.Tasks[i].TaskId);
            }

            var task1 = await minorTaskRepository.All().Where(x => x.Title == minorTask.Title).FirstOrDefaultAsync();

            Assert.True(task1.Started);
            Assert.True(task1.Finished);
        }
    }
}
