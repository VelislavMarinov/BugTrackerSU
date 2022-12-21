namespace BugTrackerSU.Services.Data.User
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Administration.Dashboard;
    using BugTrackerSU.Web.ViewModels.User;

    public interface IUserService
    {
        ManageRolesViewModel GetAllUsersAndRoles();

        string GetUserRole(ClaimsPrincipal user);

        Task SetUserRole(string userId, string roleId);
    }
}
