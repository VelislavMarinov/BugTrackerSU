namespace BugTrackerSU.Services.Data.Ticket
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Tickets;

    public interface ITicketService
    {
        Task CreateTicketAsync(CreateTicketViewModel model, string userId);

        List<TicketViewModel> GetAllUserTickets(string userId);
    }
}
