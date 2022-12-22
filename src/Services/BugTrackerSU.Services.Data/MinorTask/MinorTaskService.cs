namespace BugTrackerSU.Services.Data.MinorTask
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BugTrackerSU.Common;
    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Services.Data.Ticket;
    using BugTrackerSU.Web.ViewModels.MinorTasks;
    using Microsoft.EntityFrameworkCore;

    public class MinorTaskService : IMinorTaskService
    {
        private readonly IDeletableEntityRepository<MinorTask> minorTaskRepository;

        private readonly IDeletableEntityRepository<Ticket> ticketRepository;

        private readonly ITicketService ticketService;

        public MinorTaskService(
            IDeletableEntityRepository<MinorTask> minorTaskRepository,
            IDeletableEntityRepository<Ticket> ticketRepository,
            ITicketService ticketService)
        {
            this.minorTaskRepository = minorTaskRepository;
            this.ticketRepository = ticketRepository;
            this.ticketService = ticketService;
        }

        public async Task<bool> ChekIfUserIsAuthorizedToCreateOrSeeTask(int ticketId, string userId, string role)
        {
            if (role == GlobalConstants.AdministratorRoleName)
            {
                return true;
            }

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

        public async Task CreateMinorTaskAsync(CreateMinorTaskFormModel model, string userId)
        {
            var task = new MinorTask
            {
                AddedByUserId = userId,
                TicketId = model.TicketId,
                Title = model.Title,
                Content = model.Content,
                Started = false,
                Finished = false,
                Type = model.TaskType.ToString(),
            };

            await this.minorTaskRepository.AddAsync(task);
            await this.minorTaskRepository.SaveChangesAsync();
        }

        public async Task FinishTask(int taskId)
        {
            var task = this.minorTaskRepository.All().Where(x => x.Id == taskId).FirstOrDefault();
            task.Finished = true;

            this.minorTaskRepository.Update(task);
            await this.minorTaskRepository.SaveChangesAsync();
        }

        public async Task<AllMinorTaskViewModel> GetTicketTasksById(int ticketId, int pageNumber, int itemsPerPage)
        {
            var tasks = await this.minorTaskRepository
                .All()
                .Where(x => x.TicketId == ticketId)
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new MinorTaskViewModel
                {
                    TaskType = x.Type,
                    Title = x.Title,
                    Content = x.Content,
                    Finished = x.Finished,
                    Started = x.Started,
                })
                .ToListAsync();


            var model = new AllMinorTaskViewModel()
            {
                TicketInfo = await this.ticketService.GetTicketById(ticketId),
                TicketId = ticketId,
                PageNumber = pageNumber,
                ItemsPerPage = itemsPerPage,
                ItemsCount = await this.GetTicketTasksCount(ticketId),
                Tasks = tasks,
            };

            return model;
        }

        public async Task<int> GetTicketTasksCount(int ticketId) => await this.minorTaskRepository.All().Where(x => x.TicketId == ticketId).CountAsync();

        public async Task StartTask(int taskId)
        {
            var task = this.minorTaskRepository.All().Where(x => x.Id == taskId).FirstOrDefault();
            task.Started = true;

            this.minorTaskRepository.Update(task);
            await this.minorTaskRepository.SaveChangesAsync();
        }
    }
}
