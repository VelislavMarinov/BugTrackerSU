namespace BugTrackerSU.Services.Data.MinorTask
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.MinorTasks;

    public interface IMinorTaskService
    {
        Task CreateMinorTaskAsync(CreateMinorTaskFormModel model, string userId);

        bool ChekIfUserIsAuthorizedToCreateOrSeeTask(int ticketId, string userId, string role);

        List<MinorTaskViewModel> GetTicketTasksById(int ticketId, int pageNumber, int itemsPerPage);

        int GetTicketTasksCount(int ticketId);

        Task StartTask(int taskId);

        Task FinishTask(int taskId);
    }
}
