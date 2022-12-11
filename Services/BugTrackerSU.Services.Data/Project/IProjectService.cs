namespace BugTrackerSU.Services.Data.Project
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Projects;
    using BugTrackerSU.Web.ViewModels.User;

    public interface IProjectService
    {
        int GetUserProjectsCount(string userId, string userRole);

        Task CreateProjectAsync(CreateProjectViewModel model, string userId);

        List<ProjectViewModel> GetUserProjects(string userId, string userRole);

        List<UserViewModel> GetProjectAssignedUsers(int projectId);

        List<UserViewModel> GetProjectAssignedDevelopers(int projectId);

        bool ChekIfProjectIsValid(int projectId);

        ProjectDetailsViewModel GetProjectDetails(int projectId);
    }
}
