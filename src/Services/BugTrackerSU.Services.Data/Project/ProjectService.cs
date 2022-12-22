namespace BugTrackerSU.Services.Data.Project
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.Projects;
    using BugTrackerSU.Web.ViewModels.Tickets;
    using BugTrackerSU.Web.ViewModels.User;
    using Microsoft.EntityFrameworkCore;

    public class ProjectService : IProjectService
    {
        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly IDeletableEntityRepository<ApplicationRole> roleRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<ApplicationUserProject> userProjectRepository;
        private readonly IDeletableEntityRepository<Ticket> ticketRepository;

        public ProjectService(
            IDeletableEntityRepository<Project> projectRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<ApplicationUserProject> userProjectRepository,
            IDeletableEntityRepository<ApplicationRole> roleRepository,
            IDeletableEntityRepository<Ticket> ticketRepository)
        {
           this.projectRepository = projectRepository;
           this.userRepository = userRepository;
           this.userProjectRepository = userProjectRepository;
           this.roleRepository = roleRepository;
           this.ticketRepository = ticketRepository;
        }

        public async Task CreateProjectAsync(CreateProjectViewModel model, string userId)
        {
            var project = new Project
            {
                Title = model.Title,
                Description = model.Description,
                ProjectManagerId = userId,
            };

            foreach (var ids in model.UserIds)
            {
                var user = this.userRepository.All().Where(x => x.Id == ids).FirstOrDefault();
                if (user != null)
                {
                    var userProject = new ApplicationUserProject
                    {
                        Project = project,
                        ApplicationUser = user,
                    };
                    project.ProjectUsers.Add(userProject);
                }
            }

            await this.projectRepository.AddAsync(project);
            await this.projectRepository.SaveChangesAsync();
        }

        public async Task<AllProjectsViewModel> GetUserProjects(string userId, string userRole, int pageNumber, int itemsPerPage)
        {
            if (userRole == GlobalConstants.AdministratorRoleName)
            {
                var adminProjects = await this.projectRepository.All()
                .Include(x => x.ProjectUsers)
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new ProjectViewModel
                {
                    Title = x.Title,
                    Descripiton = x.Description,
                    ProjectId = x.Id,
                    CreatedOn = x.CreatedOn,
                })
                .ToListAsync();

                var adminModel = new AllProjectsViewModel
                {
                    PageNumber = pageNumber,
                    Projects = adminProjects,
                    ItemsPerPage = itemsPerPage,
                    ItemsCount = await this.GetUserProjectsCount(userId, userRole),
                };

                return adminModel;
            }
            else
            {
                var projects = await this.projectRepository.All()
               .Include(x => x.ProjectUsers)
               .Where(x => x.ProjectUsers.Any(u => u.ApplicationUser.Id == userId) || x.ProjectManagerId == userId)
               .OrderByDescending(x => x.Id)
               .Skip((pageNumber - 1) * itemsPerPage)
               .Take(itemsPerPage)
               .Select(x => new ProjectViewModel
               {
                   Title = x.Title,
                   Descripiton = x.Description,
                   ProjectId = x.Id,
                   CreatedOn = x.CreatedOn,
               })
               .ToListAsync();

                var model = new AllProjectsViewModel
                {
                    PageNumber = pageNumber,
                    Projects = projects,
                    ItemsPerPage = itemsPerPage,
                    ItemsCount = await this.GetUserProjectsCount(userId, userRole),
                };

                return model;
            }
        }

        public async Task<List<UserViewModel>> GetProjectAssignedUsers(int projectId)
        {
            var users = await this.userProjectRepository.
                All()
                .Where(x => x.ProjectId == projectId)
                .Select(x => x.ApplicationUser)
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                })
                .ToListAsync();

            return users;
        }

        public async Task<List<UserViewModel>> GetProjectAssignedDevelopers(int projectId)
        {
            var users = await this.userProjectRepository
                .All()
                .Where(x => x.ProjectId == projectId)
                .Select(x => x.ApplicationUser)
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    RoleId = u.Roles.Where(r => r.UserId == u.Id).Select(r => r.RoleId).FirstOrDefault(),

                })
                .ToListAsync();

            foreach (var user in users)
            {
                user.RoleName = await this.roleRepository.All().Where(x => x.Id == user.RoleId).Select(x => x.Name).FirstOrDefaultAsync();
            }

            return users;
        }

        public async Task<bool> ChekIfProjectIsValid(int projectId)
        {
            return await this.projectRepository.All().AnyAsync(x => x.Id == projectId);
        }

        public async Task<ProjectDetailsViewModel> GetProjectDetails(int projectId)
        {
            var tickets = await this.ticketRepository
                .All()
                .Where(x => x.ProjectId == projectId)
                .Select(x => new TicketViewModel
                {
                        ProjectId = x.ProjectId,
                        Title = x.Title,
                        Description = x.Description,
                        TicketId = x.Id,
                        CreatedOn = x.CreatedOn,
                        DeveloperId = x.AssignedDeveloperId,
                        SubmiterName = x.TicketSubmitter.UserName,
                        DeveloperName = x.AssignedDeveloper.UserName,
                        SubmiterId = x.AssignedDeveloperId,
                        ProjectManagerId = x.Project.ProjectManagerId,
                })
                .ToListAsync();

            var users = await this.GetProjectAssignedDevelopers(projectId);

            var model = await this.projectRepository
                .All()
                .Where(x => projectId == x.Id)
                .Select(x => new ProjectDetailsViewModel
                {
                    Description = x.Description,
                    Title = x.Title,
                    ProjectManager = x.ProjectManager.UserName,
                    Tickets = tickets,
                    AssingedUsers = users,
                })
               .FirstOrDefaultAsync();

            return model;
        }

        public async Task<int> GetUserProjectsCount(string userId, string userRole)
        {
            if (userRole == GlobalConstants.AdministratorRoleName)
            {
                int adminProjectsCount = await this.projectRepository.All().CountAsync();

                return adminProjectsCount;
            }
            else
            {
                int count = await this.projectRepository
                .All()
                .Where(x => x.ProjectManagerId == userId || x.ProjectUsers.Any(u => u.ApplicationUserId == userId))
                .CountAsync();

                return count;
            }
        }

        public async Task<List<ProjectViewModel>> GetAllProjects()
        {
            var projects = await this.projectRepository.All()
                .Select(x => new ProjectViewModel
                {
                    ProjectId = x.Id,
                    Title = x.Title,
                    Descripiton = x.Description,
                    CreatedOn = x.CreatedOn,
                })
                .ToListAsync();

            return projects;
        }
    }
}
