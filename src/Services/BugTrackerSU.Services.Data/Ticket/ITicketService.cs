namespace BugTrackerSU.Services.Data.Ticket
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.Tickets;

    public interface ITicketService
    {
        Task CreateTicketAsync(CreateTicketViewModel model, string userId);

        Task<int> GetUserTicketsCount(string userId, string userRole);

        Task<AllTicketsViewModel> GetAllUserTickets(string userId, string role, int pageNumber, int itemPerPage);

        Task EditTicketAsync(EditTicketViewModel model, string userId, string roleName);

        Task<TicketDetailsViewModel> GetTicketDetailsById(int ticketId);

        Task<TicketViewModel> GetTicketById(int ticketId);

        Task<bool> ChekIfUserIsAuthorizedToEdit(int ticketId, string userId, string role);

        Task<bool> ChekIfUserIsAuthorizedToCreateTicket(int projectId, string userId, string role);
    }
}
