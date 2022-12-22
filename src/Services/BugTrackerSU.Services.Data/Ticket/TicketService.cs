namespace BugTrackerSU.Services.Data.Ticket
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Common;
    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.Tickets;
    using Microsoft.EntityFrameworkCore;

    public class TicketService : ITicketService
    {
        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly IDeletableEntityRepository<Ticket> ticketRepository;

        public TicketService(
            IDeletableEntityRepository<Project> projectRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Ticket> ticketRepository)
        {
            this.projectRepository = projectRepository;
            this.ticketRepository = ticketRepository;
        }

        public async Task<bool> ChekIfUserIsAuthorizedToCreateTicket(int projectId, string userId, string role)
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
                var chekIfProjectContainsUser = await this.projectRepository
                    .All()
                    .Where(x => x.Id == projectId)
                    .Where(x => x.ProjectUsers.Any(x => x.ApplicationUserId == userId))
                    .FirstOrDefaultAsync();

                if (chekIfProjectContainsUser == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
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
        }

        public async Task<bool> ChekIfUserIsAuthorizedToEdit(int ticketId, string userId, string role)
        {
            var ticket = await this.ticketRepository
                .All()
                .Where(x => x.Id == ticketId)
                .Where(x => x.TicketSubmitterId == userId
                || x.AssignedDeveloperId == userId
                || x.Project.ProjectManagerId == userId)
                .FirstOrDefaultAsync();

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

        public async Task EditTicketAsync(EditTicketViewModel model, string userId, string roleName)
        {
            if (roleName == GlobalConstants.AdministratorRoleName)
            {
                var ticket = this.ticketRepository.All().Where(x => x.Id == model.TicketId).FirstOrDefault();

                if (ticket == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    ticket.AssignedDeveloperId = model.DeveloperId;
                    ticket.Priority = model.Priority.ToString();
                    ticket.TicketType = model.TicketType.ToString();
                    ticket.Status = model.Status.ToString();

                    this.ticketRepository.Update(ticket);
                    await this.ticketRepository.SaveChangesAsync();
                }
            }
            else
            {
                if (await this.ChekIfUserIsAuthorizedToEdit(model.TicketId, userId, roleName))
                {
                    var ticket = this.ticketRepository.All().Where(x => x.Id == model.TicketId).FirstOrDefault();

                    ticket.AssignedDeveloperId = model.DeveloperId;
                    ticket.Priority = model.Priority.ToString();
                    ticket.TicketType = model.TicketType.ToString();
                    ticket.Status = model.Status.ToString();

                    this.ticketRepository.Update(ticket);
                    await this.ticketRepository.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public async Task<AllTicketsViewModel> GetAllUserTickets(string userId, string userRole, int pageNumber, int itemsPerPage)
        {
            if (userRole == GlobalConstants.AdministratorRoleName)
            {
                var adminTickets = await this.ticketRepository
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
               .ToListAsync();

                var adminModel = new AllTicketsViewModel
                {
                    PageNumber = pageNumber,
                    Tickets = adminTickets,
                    ItemsPerPage = itemsPerPage,
                    ItemsCount = await this.GetUserTicketsCount(userId, userRole),
                };

                return adminModel;
            }
            else if (userRole == GlobalConstants.ProjectManagerRoleName)
            {
                var projectManagerTickets = await this.ticketRepository
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
               .ToListAsync();

                var projectMangerModel = new AllTicketsViewModel
                {
                    PageNumber = pageNumber,
                    Tickets = projectManagerTickets,
                    ItemsPerPage = itemsPerPage,
                    ItemsCount = await this.GetUserTicketsCount(userId, userRole),
                };

                return projectMangerModel;
            }
            else
            {
                var tickets = await this.ticketRepository
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
                .ToListAsync();


                var model = new AllTicketsViewModel
                {
                    PageNumber = pageNumber,
                    Tickets = tickets,
                    ItemsPerPage = itemsPerPage,
                    ItemsCount = await this.GetUserTicketsCount(userId, userRole),
                };

                return model;
            }
        }

        public async Task<TicketDetailsViewModel> GetTicketDetailsById(int ticketId)
        {
            var ticketDetails = await this.ticketRepository
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
                .FirstOrDefaultAsync();

            return ticketDetails;
        }

        public async Task<TicketViewModel> GetTicketById(int ticketId)
        {
            var ticket = await this.ticketRepository
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
                .FirstOrDefaultAsync();

            return ticket;
        }

        public async Task<int> GetUserTicketsCount(string userId, string userRole)
        {
            if (userRole == GlobalConstants.AdministratorRoleName)
            {
                int adminTicketsCount = await this.ticketRepository.All().CountAsync();

                return adminTicketsCount;
            }
            else if (userRole == GlobalConstants.ProjectManagerRoleName)
            {
                int managerTicketsCount = await this.ticketRepository.All().Where(x => x.Project.ProjectManagerId == userId).CountAsync();

                return managerTicketsCount;
            }
            else
            {
                int count = await this.ticketRepository
                .All()
                .Where(x => x.TicketSubmitterId == userId || x.AssignedDeveloperId == userId)
                .CountAsync();

                return count;
            }
        }
    }
}
