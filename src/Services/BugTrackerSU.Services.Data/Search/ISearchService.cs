namespace BugTrackerSU.Services.Data.Search
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BugTrackerSU.Web.ViewModels.Posts;
    using BugTrackerSU.Web.ViewModels.Projects;
    using BugTrackerSU.Web.ViewModels.Tickets;
    using BugTrackerSU.Web.ViewModels.User;

    public interface ISearchService
    {
        Task<IEnumerable<ProjectViewModel>> SearchForProjectByKeyword(string keyword, string userId, string userRole);

        Task<IEnumerable<TicketViewModel>> SearchForTicketByKeyword(string keyword, string userId, string userRole);

        Task<IEnumerable<UserViewModel>> SearchForUserByKeyword(string keyword);

        Task<IEnumerable<PostViewModel>> SearchForPostByKeyword(string keyword);
    }
}
