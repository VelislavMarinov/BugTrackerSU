namespace BugTrackerSU.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTrackerSU.Web.ViewModels.Roles;
    using BugTrackerSU.Web.ViewModels.User;

    public class ManageRolesViewModel
    {
        public List<RoleViewModel> Roles { get; set; }

        public List<UserViewModel> Users { get; set; }

        [Display(Name = "Select User")]
        public string UserId { get; set; }

        [Display(Name = "Select Role")]
        public string RoleId { get; set; }
    }
}
