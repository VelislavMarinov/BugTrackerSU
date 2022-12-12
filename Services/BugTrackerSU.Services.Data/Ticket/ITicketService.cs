namespace BugTrackerSU.Services.Data.Ticket
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Tickets;

    public interface ITicketService
    {
        Task CreateTicketAsync(CreateTicketViewModel model, string userId);

        int GetUserTicketsCount(string userId, string userRole);

        List<TicketViewModel> GetAllUserTickets(string userId, string role, int pageNumber, int itemPerPage);

        Task EditTicketAsync(EditTicketViewModel model, string userId);

        TicketDetailsViewModel GetTicketDetailsById(int ticketId);
    }
}
