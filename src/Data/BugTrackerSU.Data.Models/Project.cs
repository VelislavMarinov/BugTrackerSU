namespace BugTrackerSU.Data.Models
{
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;

    using BugTrackerSU.Data.Common.Models;

    using static BugTrackerSU.Common.DataConstants;

    public class Project : BaseDeletableModel<int>
    {
        public Project()
        {
            this.ProjectUsers = new List<ApplicationUserProject>();
        }

        [Required]
        [MaxLength(ProjectTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(ProjectDescriptionMaxLength)]
        public string Description { get; set; }

        public virtual ICollection<ApplicationUserProject> ProjectUsers { get; set; }

        public ApplicationUser ProjectManager { get; set; }

        [Required]
        public string ProjectManagerId { get; set; }

        public virtual ICollection<Ticket> ProjectTickets { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
