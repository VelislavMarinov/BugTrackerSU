namespace BugTrackerSU.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using BugTrackerSU.Web.ViewModels.Projects;

    public class SearchProjectFormModel
    {
        public string Keyword { get; set; }

        public IEnumerable<ProjectViewModel> Projects { get; set; }
    }
}
