namespace BugTrackerSU.Web.ViewModels.Tickets
{
    using System.Collections.Generic;

    public class AllTicketsViewModel : PagingViewModel
    {
        public List<TicketViewModel> Tickets { get; set; }
    }
}
