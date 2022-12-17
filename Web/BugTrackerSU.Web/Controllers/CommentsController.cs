namespace BugTrackerSU.Web.Controllers
{
    using BugTrackerSU.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : BaseController
    {
       [HttpGet]
       public IActionResult PostComments(int id = 1, int postId)
       {
            var formModel = new CreatePostCommentFormModel();

            return this.View(formModel);
       }
    }
}
