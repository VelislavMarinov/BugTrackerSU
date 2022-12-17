namespace BugTrackerSU.Services.Data.User
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.Administration.Dashboard;
    using BugTrackerSU.Web.ViewModels.Roles;
    using BugTrackerSU.Web.ViewModels.User;
    using Microsoft.AspNetCore.Identity;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        private readonly IDeletableEntityRepository<ApplicationRole> roleRepository;

        public UserService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<ApplicationRole> roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        public ManageRolesViewModel GetAllUsersAndRoles()
        {
            var usersViewModel = this.userRepository.All()
                .Select(x => new UserViewModel
                {
                    UserName = x.UserName,
                    Id = x.Id,
                    RoleId = x.Roles.Where(r => r.UserId == x.Id).Select(r => r.RoleId).FirstOrDefault(),
                }).ToList();

            foreach (var user in usersViewModel)
            {
                user.RoleName = this.roleRepository.All().Where(x => x.Id == user.RoleId).Select(x => x.Name).FirstOrDefault();
            }

            var rolesViewModel = this.roleRepository.All()
                .Select(x => new RoleViewModel
                {
                    RoleName = x.Name,
                    Id = x.Id,
                }).ToList();

            var model = new ManageRolesViewModel
            {
                Users = usersViewModel,
                Roles = rolesViewModel,
            };

            return model;
        }

        public string GetUserRole(ClaimsPrincipal user)
        {
            if (user.IsInRole(GlobalConstants.ProjectManagerRoleName))
            {
                return GlobalConstants.ProjectManagerRoleName;
            }
            else if (user.IsInRole(GlobalConstants.SubmitterRoleName))
            {
                return GlobalConstants.SubmitterRoleName;
            }
            else if (user.IsInRole(GlobalConstants.DeveloperRoleName))
            {
                return GlobalConstants.DeveloperRoleName;
            }
            else
            {
                return GlobalConstants.AdministratorRoleName;
            }
        }

        public async Task SetUserRole(string userId, string roleId)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);
            var userRole = new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = userId,
            };
            user.Roles.Add(userRole);

            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();
        }
    }
}
