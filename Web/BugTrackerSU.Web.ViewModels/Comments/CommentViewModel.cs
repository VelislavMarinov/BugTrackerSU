namespace BugTrackerSU.Web.ViewModels.Comments
{
    using System;

    public class CommentViewModel
    {
        public int CommentId { get; set; }

        public int PostId { get; set; }

        public string Content { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
