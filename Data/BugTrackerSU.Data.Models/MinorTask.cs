namespace BugTrackerSU.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using BugTrackerSU.Data.Common.Models;
    using BugTrackerSU.Data.Models.Enums;

    using static BugTrackerSU.Common.DataConstants;

    public class MinorTask : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(TaskTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(TaskContentMaxLength)]
        public string Content { get; set; }

        public bool Started { get; set; }

        public bool Finished { get; set; }

        [Required]
        public int TicketId { get; set; }

        [ForeignKey(nameof(TicketId))]
        public virtual Ticket Ticket { get; set; }

        [Required]
        [MaxLength(TaskTypeMaxLength)]
        public string Type { get; set; }

        [Required]
        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }
    }
}
