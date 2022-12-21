namespace BugTrackerSU.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using BugTrackerSU.Web.ViewModels.Tickets;

    public class SearchTicketFormModel
    {
        public string Keyword { get; set; }

        public IEnumerable<TicketViewModel> Tickets { get; set; }
    }
}
