namespace BugTrackerSU.Services.Data.Search
{
    using System.Collections.Generic;
    using System.Linq;

    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.Posts;
    using BugTrackerSU.Web.ViewModels.Projects;
    using BugTrackerSU.Web.ViewModels.Tickets;
    using BugTrackerSU.Web.ViewModels.User;
    using Microsoft.EntityFrameworkCore;

    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly IDeletableEntityRepository<Ticket> ticketRepository;
        private readonly IDeletableEntityRepository<Post> postRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public SearchService(
            IDeletableEntityRepository<Project> projectRepository,
            IDeletableEntityRepository<Ticket> ticketRepository,
            IDeletableEntityRepository<Post> postRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.projectRepository = projectRepository;
            this.ticketRepository = ticketRepository;
            this.postRepository = postRepository;
            this.userRepository = userRepository;
        }

        public IEnumerable<PostViewModel> SearchForPostByKeyword(string keyword)
        {
            var posts = this.postRepository
                .All()
                .Where(x => x.Title.Contains(keyword))
                .Select(x => new PostViewModel
                {
                    Id = x.Id,
                    ProjectName = x.Project.Title,
                    Title = x.Title,
                    Content = x.Content,
                    AddedByUserId = x.AddedByUserId,
                    AddedByUserUserName = x.AddedByUser.UserName,
                    CreatedOn = x.CreatedOn,
                })
                .ToList();

            return posts;
        }

        public IEnumerable<ProjectViewModel> SearchForProjectByKeyword(string keyword, string userId, string userRole)
        {
            if (userRole == "Administrator")
            {
                var adminProjects = this.projectRepository.All()
                .Include(x => x.ProjectUsers)
                .Where(x => x.Title.Contains(keyword))
                .Select(x => new ProjectViewModel
                {
                    Title = x.Title,
                    Descripiton = x.Description,
                    ProjectId = x.Id,
                    CreatedOn = x.CreatedOn,
                })
                .ToList();

                return adminProjects;
            }

            var projects = this.projectRepository.All()
                .Include(x => x.ProjectUsers)
                .Where(x => x.ProjectUsers.Any(u => u.ApplicationUser.Id == userId) || x.ProjectManagerId == userId)
                .Where(x => x.Title.Contains(keyword))
                .Select(x => new ProjectViewModel
                {
                    Title = x.Title,
                    Descripiton = x.Description,
                    ProjectId = x.Id,
                    CreatedOn = x.CreatedOn,
                })
                .ToList();

            return projects;
        }

        public IEnumerable<TicketViewModel> SearchForTicketByKeyword(string keyword, string userId, string userRole)
        {
            if (userRole == "Administrator")
            {
                var adminTickets = this.ticketRepository
               .All()
               .Where(x => x.Title.Contains(keyword))
               .Select(x => new TicketViewModel
               {
                   Title = x.Title,
                   Description = x.Description,
                   TicketId = x.Id,
                   CreatedOn = x.CreatedOn,
               })
               .ToList();

                return adminTickets;
            }

            if (userRole == "Project Manager")
            {
                var projectManagerTickets = this.ticketRepository
               .All()
               .Where(x => x.Project.ProjectManagerId == userId && x.Title.Contains(keyword))
               .Select(x => new TicketViewModel
               {
                   Title = x.Title,
                   Description = x.Description,
                   TicketId = x.Id,
                   CreatedOn = x.CreatedOn,
               })
               .ToList();

                return projectManagerTickets;
            }

            var tickets = this.ticketRepository
                .All()
                .Where(x => x.AssignedDeveloperId == userId || x.TicketSubmitterId == userId)
                .Where(x => x.Title.Contains(keyword))
                .Select(x => new TicketViewModel
                {
                    Title = x.Title,
                    Description = x.Description,
                    TicketId = x.Id,
                    CreatedOn = x.CreatedOn,
                })
                .ToList();

            return tickets;
        }

        public IEnumerable<UserViewModel> SearchForUserByKeyword(string keyword)
        {
            throw new System.NotImplementedException();
        }
    }
}
