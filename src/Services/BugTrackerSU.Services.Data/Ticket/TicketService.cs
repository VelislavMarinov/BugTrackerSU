namespace BugTrackerSU.Services.Data.Ticket
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.Comments;
    using BugTrackerSU.Web.ViewModels.Tickets;
    using Microsoft.EntityFrameworkCore;

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

        public bool ChekIfUserIsAuthorizedToCreateTicket(int projectId, string userId, string role)
        {
            if (role == GlobalConstants.AdministratorRoleName)
            {
                return true;
            }
            else if (role == GlobalConstants.DeveloperRoleName)
            {
                return false;
            }
            else if (role == GlobalConstants.SubmitterRoleName)
            {
                var chekIfProjectContainsUser = this.projectRepository
                    .All()
                    .Where(x => x.Id == projectId)
                    .Where(x => x.ProjectUsers.Any(x => x.ApplicationUserId == userId))
                    .FirstOrDefault();
            }

            var project = this.projectRepository
                .All()
                .Where(x => x.Id == projectId)
                .Where(x => x.ProjectManagerId == userId)
                .FirstOrDefault();

            if (project == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ChekIfUserIsAuthorizedToEdit(int ticketId, string userId, string role)
        {
            if (role == "Administrator")
            {
                return true;
            }

            var ticket = this.ticketRepository
                .All()
                .Where(x => x.Id == ticketId)
                .Where(x => x.TicketSubmitterId == userId
                || x.AssignedDeveloperId == userId
                || x.Project.ProjectManagerId == userId)
                .FirstOrDefault();

            if (ticket == null)
            {
                return false;
            }
            else
            {
                return true;
            }
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
            var ticket = this.ticketRepository.All().Where(x => x.Id == model.TicketId).FirstOrDefault();
            ticket.AssignedDeveloperId = model.DeveloperId;
            ticket.Priority = model.Priority.ToString();
            ticket.TicketType = model.TicketType.ToString();
            ticket.Status = model.Status.ToString();

            this.ticketRepository.Update(ticket);
            await this.ticketRepository.SaveChangesAsync();
        }

        public AllTicketsViewModel GetAllUserTickets(string userId, string userRole, int pageNumber, int itemsPerPage)
        {
            if (userRole == "Administrator")
            {
                var adminTickets = this.ticketRepository
               .All()
               .OrderByDescending(x => x.Id)
               .Skip((pageNumber - 1) * itemsPerPage)
               .Take(itemsPerPage)
               .Select(x => new TicketViewModel
               {
                   ProjectId = x.ProjectId,
                   Title = x.Title,
                   Description = x.Description,
                   TicketId = x.Id,
                   CreatedOn = x.CreatedOn,
                   DeveloperId = x.AssignedDeveloperId,
                   SubmiterName = x.TicketSubmitter.UserName,
                   DeveloperName = x.AssignedDeveloper.UserName,
                   SubmiterId = x.AssignedDeveloperId,
                   ProjectManagerId = x.Project.ProjectManagerId,
               })
               .ToList();

                var adminModel = new AllTicketsViewModel
                {
                    PageNumber = pageNumber,
                    Tickets = adminTickets,
                    ItemsPerPage = itemsPerPage,
                    ItemsCount = this.GetUserTicketsCount(userId, userRole),
                };

                return adminModel;
            }

            if (userRole == "Project Manager")
            {
                var projectManagerTickets = this.ticketRepository
               .All()
               .Where(x => x.Project.ProjectManagerId == userId)
               .OrderByDescending(x => x.Id)
               .Skip((pageNumber - 1) * itemsPerPage)
               .Take(itemsPerPage)
               .Select(x => new TicketViewModel
               {
                   ProjectId = x.ProjectId,
                   Title = x.Title,
                   Description = x.Description,
                   TicketId = x.Id,
                   CreatedOn = x.CreatedOn,
                   DeveloperId = x.AssignedDeveloperId,
                   SubmiterId = x.AssignedDeveloperId,
                   ProjectManagerId = x.Project.ProjectManagerId,
               })
               .ToList();

                var projectMangerModel = new AllTicketsViewModel
                {
                    PageNumber = pageNumber,
                    Tickets = projectManagerTickets,
                    ItemsPerPage = itemsPerPage,
                    ItemsCount = this.GetUserTicketsCount(userId, userRole),
                };

                return projectMangerModel;
            }

            var tickets = this.ticketRepository
                .All()
                .Where(x => x.AssignedDeveloperId == userId || x.TicketSubmitterId == userId)
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new TicketViewModel
                {
                    ProjectId = x.ProjectId,
                    Title = x.Title,
                    Description = x.Description,
                    TicketId = x.Id,
                    CreatedOn = x.CreatedOn,
                    DeveloperId = x.AssignedDeveloperId,
                    SubmiterId = x.AssignedDeveloperId,
                    ProjectManagerId = x.Project.ProjectManagerId,
                })
                .ToList();

            var model = new AllTicketsViewModel
            {
                PageNumber = pageNumber,
                Tickets = tickets,
                ItemsPerPage = itemsPerPage,
                ItemsCount = this.GetUserTicketsCount(userId, userRole),
            };

            return model;
        }

        public TicketDetailsViewModel GetTicketDetailsById(int ticketId)
        {
            var ticketDetails = this.ticketRepository
                .All()
                .Where(x => x.Id == ticketId)
                .Select(x => new TicketDetailsViewModel
                {
                    ProjectId = x.ProjectId,
                    DeveloperId = x.AssignedDeveloperId,
                    SubmiterId = x.TicketSubmitterId,
                    Title = x.Title,
                    TicketDescription = x.Description,
                    DeveloperName = x.AssignedDeveloper.UserName,
                    SubmitterName = x.TicketSubmitter.UserName,
                    TicketId = ticketId,
                    TicketPriority = x.Priority,
                    TicketStatus = x.Status,
                    TicketType = x.TicketType,
                    CreatedOn = x.CreatedOn,
                })
                .FirstOrDefault();

            return ticketDetails;
        }

        public TicketViewModel GetTicketById(int ticketId)
        {
            var ticket = this.ticketRepository
                .All()
                .Where(x => x.Id == ticketId)
                .Select(x => new TicketViewModel
                {
                    ProjectId = x.ProjectId,
                    Title = x.Title,
                    Description = x.Description,
                    TicketId = x.Id,
                    CreatedOn = x.CreatedOn,
                    DeveloperId = x.AssignedDeveloperId,
                    SubmiterId = x.AssignedDeveloperId,
                    ProjectManagerId = x.Project.ProjectManagerId,
                })
                .FirstOrDefault();

            return ticket;
        }

        public int GetUserTicketsCount(string userId, string userRole)
        {
            if (userRole == "Administrator")
            {
                int adminTicketsCount = this.ticketRepository.All().Count();

                return adminTicketsCount;
            }
            else if (userRole == "Project Manager")
            {
                int managerTicketsCount = this.ticketRepository.All().Where(x => x.Project.ProjectManagerId == userId).Count();

                return managerTicketsCount;
            }

            int count = this.ticketRepository
                .All()
                .Where(x => x.TicketSubmitterId == userId || x.AssignedDeveloperId == userId)
                .Count();

            return count;
        }
    }
}
