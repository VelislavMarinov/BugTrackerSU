namespace BugTrackerSU.Web.ViewModels.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTrackerSU.Web.ViewModels.Projects;

    using static BugTrackerSU.Common.DataConstants;

    public class CreatePostViewModel
    {
        [Required(ErrorMessage = "The field is required")]
        [MinLength(PostTitleMinLength, ErrorMessage = "The title must have at least {1} letters")]
        [MaxLength(PostTitleMaxLength, ErrorMessage = "The title must have maximum {1} letters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MinLength(PostContentMinLength, ErrorMessage = "The content must have at least {1} letters")]
        [MaxLength(PostContentMaxLength, ErrorMessage = "The content must have maximum {1} letters")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        public IEnumerable<ProjectViewModel> Projects { get; set; }
    }
}
