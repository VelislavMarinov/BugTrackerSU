namespace BugTrackerSU.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTrackerSU.Data.Common.Models;

    using static BugTrackerSU.Common.DataConstants;

    public class Ticket : BaseDeletableModel<int>
    {
        public Ticket()
        {
            this.Tasks = new List<MinorTask>();
        }


        [Required]
        [MaxLength(TicketTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(TicketDescriptionMaxLength)]
        public string Description { get; set; }

        public virtual ApplicationUser TicketSubmitter { get; set; }

        [Required]
        public string TicketSubmitterId { get; set; }

        public virtual ApplicationUser AssignedDeveloper { get; set; }

        [Required]
        public string AssignedDeveloperId { get; set; }

        [Required]
        [MaxLength(TicketStatusMaxLength)]
        public string Status { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        [Required]
        [MaxLength(TicketPriorityMaxLength)]
        public string Priority { get; set; }

        [Required]
        [MaxLength(TicketTypeMaxLength)]
        public string TicketType { get; set; }

        public ICollection<MinorTask> Tasks { get; set; }
    }
}
