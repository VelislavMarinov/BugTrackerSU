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

        public ProjectService(
            IDeletableEntityRepository<Project> projectRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<ApplicationUserProject> userProjectRepository,
            IDeletableEntityRepository<ApplicationRole> roleRepository)
        {
           this.projectRepository = projectRepository;
           this.userRepository = userRepository;
           this.userProjectRepository = userProjectRepository;
           this.roleRepository = roleRepository;
        }

        public async Task CreateProjectAsync(CreateProjectViewModel model, string userId)
        {
            var project = new Project
            {
                Title = model.Title,
                Description = model.Description,
                ProjectManagerId = userId,
            };

            foreach (var modelUser in model.AllUsers)
            {
                if (modelUser.Selected == true)
                {
                    var user = this.userRepository.All().Where(x => x.Id == modelUser.Id).FirstOrDefault();
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
            }

            await this.projectRepository.AddAsync(project);
            await this.projectRepository.SaveChangesAsync();
        }

        public AllProjectsViewModel GetUserProjects(string userId, string userRole, int pageNumber, int itemsPerPage)
        {
            if (userRole == "Administrator")
            {
                var adminProjects = this.projectRepository.All()
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
                .ToList();

                var adminModel = new AllProjectsViewModel
                {
                    PageNumber = pageNumber,
                    Projects = adminProjects,
                    ItemsPerPage = itemsPerPage,
                    ItemsCount = this.GetUserProjectsCount(userId, userRole),
                };

                return adminModel;
            }

            var projects = this.projectRepository.All()
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
                .ToList();

            var model = new AllProjectsViewModel
            {
                PageNumber = pageNumber,
                Projects = projects,
                ItemsPerPage = itemsPerPage,
                ItemsCount = this.GetUserProjectsCount(userId, userRole),
            };

            return model;
        }

        public List<UserViewModel> GetProjectAssignedUsers(int projectId)
        {
            var users = this.userProjectRepository.
                All()
                .Where(x => x.ProjectId == projectId)
                .Select(x => x.ApplicationUser)
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                })
                .ToList();

            return users;
        }

        public List<UserViewModel> GetProjectAssignedDevelopers(int projectId)
        {
            var role = this.roleRepository.All().Where(x => x.Name == GlobalConstants.DeveloperRoleName).FirstOrDefault();

            var users = this.userProjectRepository
                .All()
                .Where(x => x.ProjectId == projectId)
                .Select(x => x.ApplicationUser)
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    RoleName = role.Name,

                }).ToList();

            return users;
        }

        public bool ChekIfProjectIsValid(int projectId)
        {
            return this.projectRepository.All().Any(x => x.Id == projectId);
        }

        public ProjectDetailsViewModel GetProjectDetails(int projectId)
        {
            var model = this.projectRepository.
                All()
                .Where(x => projectId == x.Id)
                .Select(x => new ProjectDetailsViewModel
                {
                    Description = x.Description,
                    Title = x.Title,
                    ProjectManager = x.ProjectManager.UserName,
                    Tickets = x.ProjectTickets.Select(p => new TicketViewModel
                    {
                        ProjectId = p.ProjectId,
                        Title = p.Title,
                        Description = p.Description,
                        TicketId = p.Id,
                        CreatedOn = p.CreatedOn,
                        DeveloperId = p.AssignedDeveloperId,
                        SubmiterName = p.TicketSubmitter.UserName,
                        DeveloperName = p.AssignedDeveloper.UserName,
                        SubmiterId = p.AssignedDeveloperId,
                        ProjectManagerId = p.Project.ProjectManagerId,
                    }).ToList(),
                    AssingedUsers = this.GetProjectAssignedDevelopers(projectId),
                })
               .FirstOrDefault();

            return model;
        }

        public int GetUserProjectsCount(string userId, string userRole)
        {
            if (userRole == "Administrator")
            {
                int adminProjectsCount = this.projectRepository.All().Count();

                return adminProjectsCount;
            }

            int count = this.projectRepository
                .All()
                .Where(x => x.ProjectManagerId == userId || x.ProjectUsers.Any(u => u.ApplicationUserId == userId))
                .Count();

            return count;
        }

        public List<ProjectViewModel> GetAllProjects()
        {
            var projects = this.projectRepository.All()
                .Select(x => new ProjectViewModel
                {
                    ProjectId = x.Id,
                    Title = x.Title,
                    Descripiton = x.Description,
                    CreatedOn = x.CreatedOn,
                })
                .ToList();

            return projects;
        }
    }
}
