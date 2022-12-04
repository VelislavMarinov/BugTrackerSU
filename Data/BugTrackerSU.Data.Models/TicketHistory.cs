namespace BugTrackerSU.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BugTrackerSU.Data.Common.Models;

    using static BugTrackerSU.Common.DataConstants;

    public class TicketHistory : BaseDeletableModel<int>
    {
        public Ticket Ticket { get; set; }

        [Required]
        public int TicketId { get; set; }

        [MaxLength(TicketDeveloperNameMaxLength)]
        public string AssignedDeveloperNewValue { get; set; } = null!;

        [Required]
        [MaxLength(TicketDeveloperNameMaxLength)]
        public string AssignedDeveloperOldValue { get; set; }

        [MaxLength(TicketPriorityMaxLength)]
        public string TicketPriorityNewValue { get; set; } = null!;

        [Required]
        [MaxLength(TicketPriorityMaxLength)]
        public string TicketPriorityOldValue { get; set; }

        [MaxLength(TicketStatusMaxLength)]
        public string TicketStatusNewValue { get; set; } = null!;

        [Required]
        [MaxLength(TicketStatusMaxLength)]
        public string TicketStatusOldValue { get; set; }

        [MaxLength(TicketTypeMaxLength)]
        public string TicketTypeNewValue { get; set; } = null!;

        [Required]
        [MaxLength(TicketTypeMaxLength)]
        public string TicketTypeOldValue { get; set; }
    }
}
