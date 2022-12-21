namespace BugTrackerSU.Web.ViewModels.MinorTasks
{
    using BugTrackerSU.Web.ViewModels.Tickets;
    using System.Collections.Generic;

    public class AllMinorTaskViewModel : PagingViewModel
    {
        public List<MinorTaskViewModel> Tasks { get; set; }

        public TicketViewModel TicketInfo { get; set; }

        public string TicketName { get; set; }

        public int TicketId { get; set; }
    }
}
