namespace BugTrackerSU.Web.ViewModels.User
{
    using System.Collections.Generic;

    using BugTrackerSU.Web.ViewModels.Roles;

    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public bool Selected { get; set; }
    }
}
