namespace BugTrackerSU.Web.ViewModels.Projects
{
    using System;

    public class ProjectViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription => this.Description?.Length > 20 ? this.Description?.Substring(0, 20) + "…" : this.Description;

        public int ProjectId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
