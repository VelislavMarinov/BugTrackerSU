namespace BugTrackerSU.Web.ViewModels.Tickets
{
    using System;
    using System.Collections.Generic;

    using BugTrackerSU.Web.ViewModels.Comments;
    using BugTrackerSU.Web.ViewModels.TicketsHistory;

    public class TicketDetailsViewModel
    {
        public int TicketId { get; set; }

        public string Title { get; set; }

        public string TicketDescription { get; set; }

        public string SubmitterName { get; set; }

        public string DeveloperName { get; set; }

        public string TicketStatus { get; set; }

        public string TicketPriority { get; set; }

        public string TicketType { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public List<CommentViewModel> Comments { get; set; }

        public TicketHistoryViewModel TicketHistory { get; set; }

        public CreateCommentViewModel CreateComment { get; set; }
    }
}
