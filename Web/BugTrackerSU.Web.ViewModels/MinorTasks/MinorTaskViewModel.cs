namespace BugTrackerSU.Web.ViewModels.MinorTasks
{
    public class MinorTaskViewModel
    {
        public int TaskId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string AddebyUser { get; set; }

        public bool Started { get; set; }

        public bool Finished { get; set; }

        public string TaskType { get; set; }

    }
}
