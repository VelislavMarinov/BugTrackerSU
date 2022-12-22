namespace BugTrackerSU.Services.Data.Project
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Projects;
    using BugTrackerSU.Web.ViewModels.User;

    public interface IProjectService
    {
        Task<List<ProjectViewModel>> GetAllProjects();

        Task<int> GetUserProjectsCount(string userId, string userRole);

        Task CreateProjectAsync(CreateProjectViewModel model, string userId);

        Task<AllProjectsViewModel> GetUserProjects(string userId, string userRole, int pageNumber, int itemPerPage);

        Task<List<UserViewModel>> GetProjectAssignedUsers(int projectId);

        Task<List<UserViewModel>> GetProjectAssignedDevelopers(int projectId);

        Task<bool> ChekIfProjectIsValid(int projectId);

        Task<ProjectDetailsViewModel> GetProjectDetails(int projectId);
    }
}
