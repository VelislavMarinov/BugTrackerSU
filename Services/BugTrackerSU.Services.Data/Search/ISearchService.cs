namespace BugTrackerSU.Services.Data.Search
{
    using BugTrackerSU.Web.ViewModels.Posts;
    using BugTrackerSU.Web.ViewModels.Projects;
    using BugTrackerSU.Web.ViewModels.Tickets;
    using BugTrackerSU.Web.ViewModels.User;
    using System.Collections.Generic;

    public interface ISearchService
    {
        IEnumerable<ProjectViewModel> SearchForProjectByKeyword();

        IEnumerable<TicketViewModel> SearchForTicketByKeyword();

        IEnumerable<UserViewModel> SearchForUserByKeyword();

        IEnumerable<PostViewModel> SearchForPostByKeyword();
    }
}
