namespace BugTrackerSU.Web.ViewModels.Tickets
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BugTrackerSU.Data.Models.Enums;
    using BugTrackerSU.Web.ViewModels.Projects;
    using BugTrackerSU.Web.ViewModels.User;

    using static BugTrackerSU.Common.DataConstants;

    public class CreateTicketViewModel
    {
        public CreateTicketViewModel()
        {
            this.AsignedProjectDevelopers = new List<UserViewModel>();
        }

        [Required(ErrorMessage = "The field is required")]
        [MinLength(TicketTitleMinLength, ErrorMessage = "The title must have at least {1} letters")]
        [MaxLength(TicketTitleMaxLength, ErrorMessage = "The title must have maximum {1} letters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MinLength(TicketDescriptionMinLength, ErrorMessage = "The description must have at least {1} letters")]
        [MaxLength(TicketDescriptionMaxLength, ErrorMessage = "The description must have maximum {1} letters")]
        public string Description { get; set; }

        public ICollection<UserViewModel> AsignedProjectDevelopers { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [Display(Name = "Assign developer")]
        public string DeveloperId { get; set; }

        [Required(ErrorMessage = "The field is required")]
        public int ProjectId { get; set; }

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
