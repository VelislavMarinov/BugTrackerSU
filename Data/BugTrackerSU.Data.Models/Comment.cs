namespace BugTrackerSU.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BugTrackerSU.Data.Common.Models;

    using static BugTrackerSU.Common.DataConstants;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(CommentContentMaxLength)]
        public string Content { get; set; }

        public int TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }

        [Required]
        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }
    }
}
