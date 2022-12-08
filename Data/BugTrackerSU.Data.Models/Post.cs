namespace BugTrackerSU.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTrackerSU.Data.Common.Models;

    using static BugTrackerSU.Common.DataConstants;

    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.Comments = new List<Comment>();
        }

        [Required]
        [MaxLength(PostTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(PostContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
