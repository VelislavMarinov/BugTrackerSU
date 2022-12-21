namespace BugTrackerSU.Web.ViewModels.Categories
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShortDescription => this.Description?.Length > 50 ? this.Description?.Substring(0, 50) + "…" : this.Description;

        public string AddedBy { get; set; }

        public string ImageUrl { get; set; }
    }
}
