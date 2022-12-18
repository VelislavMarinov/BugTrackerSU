namespace BugTrackerSU.Services.Data.MinorTask
{
    using System.Threading.Tasks;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Web.ViewModels.MinorTasks;
    using System.Linq;

    public class MinorTaskService : IMinorTaskService
    {
        private readonly IDeletableEntityRepository<MinorTask> minorTaskRepository;

        private readonly IDeletableEntityRepository<Ticket> ticketRepository;

        public MinorTaskService(
            IDeletableEntityRepository<MinorTask> minorTaskRepository,
            IDeletableEntityRepository<Ticket> ticketRepository)
        {
            this.minorTaskRepository = minorTaskRepository;
            this.ticketRepository = ticketRepository;
        }

        public bool ChekIfUserIsAuthorizedToCreateTask(int ticketId, string userId, string role)
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
    }
}
