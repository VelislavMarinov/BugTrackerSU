// ReSharper disable VirtualMemberCallInConstructor
namespace BugTrackerSU.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BugTrackerSU.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.UserProjects = new List<ApplicationUserProject>();
            this.Tickets = new List<Ticket>();
            this.Comments = new List<Comment>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<ApplicationUserProject> UserProjects { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
