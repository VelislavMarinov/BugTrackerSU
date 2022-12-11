namespace BugTrackerSU.Web.ViewModels.Projects
{
    using System.Collections.Generic;

    using BugTrackerSU.Web.ViewModels.Tickets;
    using BugTrackerSU.Web.ViewModels.User;

    public class ProjectDetailsViewModel
    {
        public ProjectDetailsViewModel()
        {
            this.Tickets = new List<TicketViewModel>();
            this.AssingedUsers = new List<UserViewModel>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<TicketViewModel> Tickets { get; set; }

        public List<UserViewModel> AssingedUsers { get; set; }
    }
}
