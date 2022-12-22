namespace BugTrackerSU.Services.Data.Ticket
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Tickets;

    public interface ITicketService
    {
        Task CreateTicketAsync(CreateTicketViewModel model, string userId);

        int GetUserTicketsCount(string userId, string userRole);

        AllTicketsViewModel GetAllUserTickets(string userId, string role, int pageNumber, int itemPerPage);

        Task EditTicketAsync(EditTicketViewModel model, string userId, string roleName);

        TicketDetailsViewModel GetTicketDetailsById(int ticketId);

        TicketViewModel GetTicketById(int ticketId);

        bool ChekIfUserIsAuthorizedToEdit(int ticketId, string userId, string role);

        bool ChekIfUserIsAuthorizedToCreateTicket(int projectId, string userId, string role);
    }
}
