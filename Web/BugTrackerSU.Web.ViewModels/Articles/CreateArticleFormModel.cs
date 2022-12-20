namespace BugTrackerSU.Web.ViewModels.Articles
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTrackerSU.Web.ViewModels.Categories;

    using static BugTrackerSU.Common.DataConstants;

    public class CreateArticleFormModel
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MinLength(ArticleNameMinLength, ErrorMessage = "The name must have at least {1} letters")]
        [MaxLength(ArticleNameMaxLength, ErrorMessage = "The name must have maximum {1} letters")]
        [Display(Name = "Article Name")]
        public string Name { get; set; }

        [Required]
        [MinLength(ArticleDescriptionMinLength, ErrorMessage = "The description must have at least {1} letters")]
        [MaxLength(ArticleDescriptionMaxLength, ErrorMessage = "The description must have maximum {1} letters")]
        [Display(Name = "Article Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Video Url")]
        [Url]
        public string VideoUrl { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}
