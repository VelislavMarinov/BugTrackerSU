namespace BugTrackerSU.Web.ViewModels.MinorTasks
{
    using System.ComponentModel.DataAnnotations;

    using BugTrackerSU.Data.Models.Enums;

    using static BugTrackerSU.Common.DataConstants;

    public class CreateMinorTaskFormModel
    {
        [Required(ErrorMessage = "The field is required")]
        [MinLength(TaskTitleMinLength, ErrorMessage = "The title must have at least {1} letters")]
        [MaxLength(TaskTitleMaxLength, ErrorMessage = "The title must have maximum {1} letters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [MinLength(TaskContentMinLength, ErrorMessage = "The content must have at least {1} letters")]
        [MaxLength(TaskContentMaxLength, ErrorMessage = "The content must have maximum {1} letters")]
        public string Content { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [Display(Name = "Type of Task")]
        public TaskType TicketType { get; set; }

        public int TicketId { get; set; }
    }
}
