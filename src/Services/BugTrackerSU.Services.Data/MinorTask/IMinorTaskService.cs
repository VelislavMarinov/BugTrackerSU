namespace BugTrackerSU.Services.Data.MinorTask
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.MinorTasks;

    public interface IMinorTaskService
    {
        Task CreateMinorTaskAsync(CreateMinorTaskFormModel model, string userId);

        Task<bool> ChekIfUserIsAuthorizedToCreateOrSeeTask(int ticketId, string userId, string role);

        Task<AllMinorTaskViewModel> GetTicketTasksById(int ticketId, int pageNumber, int itemsPerPage);

        Task<int> GetTicketTasksCount(int ticketId);

        Task StartTask(int taskId);

        Task FinishTask(int taskId);
    }
}
