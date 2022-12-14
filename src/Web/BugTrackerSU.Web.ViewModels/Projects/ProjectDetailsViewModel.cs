namespace BugTrackerSU.Web.ViewModels.Projects
{
    using System.Collections.Generic;

    using BugTrackerSU.Web.ViewModels.Tickets;
    using BugTrackerSU.Web.ViewModels.User;

    public class ProjectDetailsViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ProjectManager { get; set; }

        public List<TicketViewModel> Tickets { get; set; }

        public List<UserViewModel> AssingedUsers { get; set; }
    }
}
