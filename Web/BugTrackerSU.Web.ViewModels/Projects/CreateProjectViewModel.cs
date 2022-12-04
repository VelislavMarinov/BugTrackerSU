namespace BugTrackerSU.Web.ViewModels.Projects
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTrackerSU.Web.ViewModels.User;

    using static BugTrackerSU.Common.DataConstants;

    public class CreateProjectViewModel
    {
        [Required(ErrorMessage = "The field is required")]
        [MinLength(ProjectTitleMinLength, ErrorMessage = "The title must have at least {1} letters")]
        [MaxLength(ProjectTitleMaxLength, ErrorMessage = "The title must have maximum {1} letters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MinLength(ProjectDescriptionMinLength, ErrorMessage = "The description must have at least {1} letters")]
        [MaxLength(ProjectDescriptionMaxLength, ErrorMessage = "The description must have maximum {1} letters")]
        public string Description { get; set; }

        public List<UserViewModel> AllUsers { get; set; }
    }
}
