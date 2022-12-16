namespace BugTrackerSU.Web.Controllers
{
    using System.Linq;

    using BugTrackerSU.Common;
    using BugTrackerSU.Services.Data.Search;
    using BugTrackerSu.Web;
    using BugTrackerSU.Web.ViewModels.Search;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : BaseController
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet]
        public IActionResult SearchProject() => this.View();

        [HttpPost]
        public IActionResult SearchProject(SearchProjectFormModel model)
        {
            var userId = this.User.GetId();

            var userRole = string.Empty;

            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                userRole = GlobalConstants.AdministratorRoleName;
            }

            var projects = this.searchService.SearchForProjectByKeyword(model.Keyword, userId, userRole);

            if (projects.Any())
            {
                model.Projects = projects;
                model.Keyword = string.Empty;
                return this.View("FoundProject", model);
            }
            else
            {
                this.TempData["Message"] = "There was no Project title found with the given keyword.";
                return this.View();
            }
        }

        [HttpGet]
        public IActionResult SearchTicket() => this.View();

        [HttpPost]
        public IActionResult SearchTicket(SearchTicketFormModel model)
        {
            var userId = this.User.GetId();

            var userRole = string.Empty;

            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                userRole = GlobalConstants.AdministratorRoleName;
            }

            if (this.User.IsInRole(GlobalConstants.ProjectManagerRoleName))
            {
                userRole = GlobalConstants.ProjectManagerRoleName;
            }

            var tickets = this.searchService.SearchForTicketByKeyword(model.Keyword, userId, userRole);

            if (tickets.Any())
            {
                model.Tickets = tickets;
                model.Keyword = string.Empty;
                return this.View("FoundTicket", model);
            }
            else
            {
                this.TempData["Message"] = "There was no Ticket title found with the given keyword.";
                return this.View();
            }
        }

        [HttpGet]
        public IActionResult SearchPost() => this.View();

        [HttpPost]
        public IActionResult SearchPost(SearchPostFormModel model)
        {

            var posts = this.searchService.SearchForPostByKeyword(model.Keyword);

            if (posts.Any())
            {
                model.Posts = posts;
                model.Keyword = string.Empty;
                return this.View("FoundPost", model);
            }
            else
            {
                this.TempData["Message"] = "There was no Post title found with the given keyword.";
                return this.View();
            }
        }
    }
}
