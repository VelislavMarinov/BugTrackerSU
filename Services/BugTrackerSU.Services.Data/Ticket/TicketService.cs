namespace BugTrackerSU.Services.Data.Ticket
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Services.Data.TicketHistory;
    using BugTrackerSU.Web.ViewModels.Comments;
    using BugTrackerSU.Web.ViewModels.Tickets;

    public class TicketService : ITicketService
    {
        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Ticket> ticketRepository;
        private readonly ITicketHistoryService ticketHistoryService;

        public TicketService(
            IDeletableEntityRepository<Project> projectRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository, 
            IDeletableEntityRepository<Ticket> ticketRepository)
        {
            this.projectRepository = projectRepository;
            this.userRepository = userRepository;
            this.ticketRepository = ticketRepository;
        }

        public async Task CreateTicketAsync(CreateTicketViewModel model, string userId)
        {
            var ticket = new Ticket
            {
                Title = model.Title,
                Description = model.Description,
                AssignedDeveloperId = model.DeveloperId,
                TicketSubmitterId = userId,
                TicketType = model.TicketType.ToString(),
                Priority = model.Priority.ToString(),
                Status = model.Status.ToString(),
                ProjectId = model.ProjectId,
            };

            await this.ticketRepository.AddAsync(ticket);
            await this.ticketRepository.SaveChangesAsync();
        }

        public async Task EditTicketAsync(EditTicketViewModel model, string userId)
        {
            await this.ticketHistoryService.CreateTicketHistoryAsync(model.TicketId, model);

            var ticket = this.ticketRepository.All().Where(x => x.Id == model.TicketId).FirstOrDefault();
            ticket.AssignedDeveloperId = model.DeveloperId;
            ticket.Priority = model.Priority.ToString();
            ticket.TicketType = model.TicketType.ToString();
            ticket.Status = model.Status.ToString();

            this.ticketRepository.Update(ticket);
            await this.ticketRepository.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public List<TicketViewModel> GetAllUserTickets(string userId)
        {
            var tickets = this.ticketRepository
                .All()
                .Where(x => x.AssignedDeveloperId == userId || x.TicketSubmitterId == userId)
                .Select(x => new TicketViewModel
                {
                    Title = x.Title,
                    Description = x.Description,
                    TicketId = x.Id,
                    CreatedOn = x.CreatedOn,
                })
                .ToList();

            return tickets;
        }

        public TicketDetailsViewModel GetTicketDetailsById(int ticketId)
        {
            var ticketDetails = this.ticketRepository
                .All()
                .Where(x => x.Id == ticketId)
                .Select(x => new TicketDetailsViewModel
                {
                    Title = x.Title,
                    TicketDescription = x.Description,
                    DeveloperName = x.AssignedDeveloper.UserName,
                    TicketId = ticketId,
                    TicketPriority = x.Priority,
                    TicketStatus = x.Status,
                    TicketType = x.TicketType,
                    CreatedOn = x.CreatedOn,
                    UpdatedOn = (DateTime)x.ModifiedOn,
                    Comments = x.Comments
                                 .Select(c => new CommentViewModel
                                 {
                                    CommentId = c.Id,
                                    Content = c.Content,
                                    TicketId = x.Id,
                                    UserId = c.AddedByUserId,
                                    UserName = c.AddedByUser.UserName,
                                 })
                                 .ToList(),
                })
                .FirstOrDefault();

            return ticketDetails;
        }
    }
}
