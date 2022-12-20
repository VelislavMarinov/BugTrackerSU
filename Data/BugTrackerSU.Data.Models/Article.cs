namespace BugTrackerSU.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BugTrackerSU.Data.Common.Models;

    using static BugTrackerSU.Common.DataConstants;

    public class Article : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(ArticleNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(ArticleDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [Url]
        public string VideoUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

    }
}
