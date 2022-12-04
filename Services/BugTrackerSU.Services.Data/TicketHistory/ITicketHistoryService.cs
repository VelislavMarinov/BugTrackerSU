namespace BugTrackerSU.Services.Data.TicketHistory
{
    using System;
    using System.Threading.Tasks;

    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.Tickets;

    public interface ITicketHistoryService
    {
        Task CreateTicketHistoryAsync(int tikcetId, EditTicketViewModel model);
    }
}
