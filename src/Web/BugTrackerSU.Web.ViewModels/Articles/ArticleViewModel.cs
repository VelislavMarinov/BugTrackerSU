namespace BugTrackerSU.Web.ViewModels.Articles
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ShortDescription => this.Description?.Length > 80 ? this.Description?.Substring(0, 80) + "…" : this.Description;

        public string CreatedBy { get; set; }

        public string VideoUrl { get; set; }

        public string CreatedById { get; set; }

        public string CategoryName { get; set; }
    }
}
