namespace BugTrackerSU.Web.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static BugTrackerSU.Common.DataConstants;

    public class EditCategoryFormModel
    {
        [Required]
        [MinLength(CategoryNameMinLength, ErrorMessage = "The name must have at least {1} letters")]
        [MaxLength(CategoryNameMaxLength, ErrorMessage = "The name must have maximum {1} letters")]
        public string Name { get; set; }

        [Required]
        [MinLength(CategoryDescriptionMinLength, ErrorMessage = "The description must have at least {1} letters")]
        [MaxLength(CategoryDescriptionMaxLength, ErrorMessage = "The description must have maximum {1} letters")]
        public string Description { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }
    }
}
