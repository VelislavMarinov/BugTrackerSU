namespace BugTrackerSU.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using static BugTrackerSU.Common.DataConstants;

    public class CreateCommentViewModel
    {
        [Required]
        [MinLength(CommentContentMinLength, ErrorMessage = "The content must have at least {1} letters")]
        [MaxLength(CommentContentMaxLength, ErrorMessage = "The content must have maximum {1} letters")]
        public string Content { get; set; }

        public int? TicketId { get; set; }

        public int? PostId { get; set; }
    }
}
