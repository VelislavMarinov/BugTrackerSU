﻿namespace BugTrackerSU.Services.Data.Ticket
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.Tickets;

    public class TicketService : ITicketService
    {
        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Ticket> ticketRepository;

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
    }
}
