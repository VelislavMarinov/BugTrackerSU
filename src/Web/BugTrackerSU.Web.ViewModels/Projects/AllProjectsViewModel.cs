namespace BugTrackerSU.Web.ViewModels.Projects
{
    using System.Collections.Generic;

    public class AllProjectsViewModel : PagingViewModel
    {
        public List<ProjectViewModel> Projects { get; set; }
    }
}
