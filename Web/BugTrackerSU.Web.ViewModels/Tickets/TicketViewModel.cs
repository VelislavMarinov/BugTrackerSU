namespace BugTrackerSU.Web.ViewModels.Tickets
{
    using System;

    public class TicketViewModel
    {
        public int TicketId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string DeveloperName { get; set; }

        public string SubmiterName { get; set; }

        public string TicketStatus { get; set; }
    }
}
