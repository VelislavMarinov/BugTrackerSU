namespace BugTrackerSU.Services.Data.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Administration.Dashboard;
    using BugTrackerSU.Web.ViewModels.User;

    public interface IUserService
    {
        ManageRolesViewModel GetAllUsersAndRoles();

        Task SetUserRole(string userId, string roleId);
    }
}
