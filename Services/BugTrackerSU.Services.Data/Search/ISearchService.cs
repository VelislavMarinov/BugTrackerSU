namespace BugTrackerSU.Services.Data.Search
{
    using System.Collections.Generic;

    using BugTrackerSU.Web.ViewModels.Posts;
    using BugTrackerSU.Web.ViewModels.Projects;
    using BugTrackerSU.Web.ViewModels.Tickets;
    using BugTrackerSU.Web.ViewModels.User;

    public interface ISearchService
    {
        IEnumerable<ProjectViewModel> SearchForProjectByKeyword(string keyword);

        IEnumerable<TicketViewModel> SearchForTicketByKeyword(string keyword);

        IEnumerable<UserViewModel> SearchForUserByKeyword(string keyword);

        IEnumerable<PostViewModel> SearchForPostByKeyword(string keyword);
    }
}
