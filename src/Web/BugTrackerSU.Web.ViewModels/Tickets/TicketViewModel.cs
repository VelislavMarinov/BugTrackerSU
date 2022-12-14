namespace BugTrackerSU.Web.ViewModels.Tickets
{
    using System;

    public class TicketViewModel
    {
        public int ProjectId { get; set; }

        public string ProjectManagerId { get; set; }

        public string SubmiterId { get; set; }

        public string DeveloperId { get; set; }

        public int TicketId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription => this.Description?.Length > 20 ? this.Description?.Substring(0, 20) + "…" : this.Description;

        public DateTime CreatedOn { get; set; }

        public string DeveloperName { get; set; }

        public string SubmiterName { get; set; }

        public string TicketStatus { get; set; }
    }
}
