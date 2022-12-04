namespace BugTrackerSU.Services.Data.TicketHistory
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.Tickets;

    public class TicketHistoryService : ITicketHistoryService
    {
        private readonly IDeletableEntityRepository<TicketHistory> ticketHistoryRepository;
        private readonly IDeletableEntityRepository<Ticket> ticketRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public TicketHistoryService(
            IDeletableEntityRepository<TicketHistory> ticketHistoryRepository,
            IDeletableEntityRepository<Ticket> ticketRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.ticketHistoryRepository = ticketHistoryRepository;
            this.ticketRepository = ticketRepository;
            this.userRepository = userRepository;
        }

        public async Task CreateTicketHistoryAsync(int tikcetId, EditTicketViewModel model)
        {
            var ticket = this.ticketRepository
                .All()
                .Where(x => x.Id == tikcetId)
                .Select(x => new
                {
                    DeveloperName = x.AssignedDeveloper.UserName,
                    SubmiterName = x.TicketSubmitter.UserName,
                    Status = x.Status,
                    Priority = x.Priority,
                    TicketType = x.TicketType,
                })
                .FirstOrDefault();

            var developerName = this.userRepository
                .All()
                .Where(x => model.DeveloperId == x.Id)
                .Select(x => new
                {
                    UserName = x.UserName,
                })
                .FirstOrDefault();

            var ticketHistory = new TicketHistory
            {
                TicketId = tikcetId,
                AssignedDeveloperOldValue = ticket.DeveloperName,
                AssignedDeveloperNewValue = developerName.UserName,
                TicketStatusOldValue = ticket.Status,
                TicketStatusNewValue = model.Status.ToString(),
                TicketPriorityOldValue = ticket.Priority,
                TicketPriorityNewValue = model.Priority.ToString(),
                TicketTypeOldValue = ticket.TicketType,
                TicketTypeNewValue = model.TicketType.ToString(),
            };

            await this.ticketHistoryRepository.AddAsync(ticketHistory);
            await this.ticketHistoryRepository.SaveChangesAsync();
        }
    }
}
