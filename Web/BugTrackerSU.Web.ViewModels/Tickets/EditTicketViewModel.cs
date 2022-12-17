namespace BugTrackerSU.Web.ViewModels.Tickets
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTrackerSU.Data.Models.Enums;
    using BugTrackerSU.Web.ViewModels.User;

    using static BugTrackerSU.Common.DataConstants;

    public class EditTicketViewModel
    {
        public EditTicketViewModel()
        {
            this.AsignedProjectDevelopers = new List<UserViewModel>();
        }

        public int TicketId { get; set; }

        public string UserId { get; set; }

        public ICollection<UserViewModel> AsignedProjectDevelopers { get; set; }

        [Required]
        [Display(Name = "Assign developer")]
        public string DeveloperId { get; set; }

        [Display(Name = "Assign submiter")]
        public string Submiter { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [Display(Name = "Status of ticket")]
        public TicketStatus Status { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [Display(Name = "Priority of ticket")]
        public TicketPriority Priority { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [Display(Name = "Type of ticket")]
        public TicketType TicketType { get; set; }
    }
}
