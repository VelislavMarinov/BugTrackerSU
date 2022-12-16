﻿namespace BugTrackerSU.Web.Controllers
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
    }
}