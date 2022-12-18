namespace BugTrackerSU.Services.Data.MinorTask
{
    using System.Threading.Tasks;

    using BugTrackerSU.Web.ViewModels.MinorTasks;

    public interface IMinorTaskService
    {
        Task CreateMinorTaskAsync(CreateMinorTaskFormModel model, string userId);

        bool ChekIfUserIsAuthorizedToCreateTask(int ticketId, string userId, string role);
    }
}
