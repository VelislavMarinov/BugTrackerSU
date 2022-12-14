namespace BugTrackerSU.Web.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static BugTrackerSU.Common.DataConstants;

    public class EditCategoryFormModel
    {
        [Required]
        [MinLength(CategoryNameMinLength, ErrorMessage = "The name must have at least {1} letters")]
        [MaxLength(CategoryNameMaxLength, ErrorMessage = "The name must have maximum {1} letters")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required]
        [MinLength(CategoryDescriptionMinLength, ErrorMessage = "The description must have at least {1} letters")]
        [MaxLength(CategoryDescriptionMaxLength, ErrorMessage = "The description must have maximum {1} letters")]
        [Display(Name = "Category Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        [Url]
        public string ImageUrl { get; set; }
    }
}
