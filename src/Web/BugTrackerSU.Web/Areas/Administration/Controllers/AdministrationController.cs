namespace BugTrackerSU.Web.Areas.Administration.Controllers
{
    using BugTrackerSU.Common;
    using BugTrackerSU.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
