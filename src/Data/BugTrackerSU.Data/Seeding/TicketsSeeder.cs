namespace BugTrackerSU.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Data.Models;

    public class TicketsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Tickets.Any())
            {
                return;
            }

            var tickets = new List<Ticket>
            {
                new Ticket
                {
                    Title = "Delete button",
                    Description = "Delete button for Sculptures not working",
                    AssignedDeveloperId = dbContext.Users.Where(x => x.UserName == "Kaloqn").FirstOrDefault().Id,
                    AssignedDeveloper = dbContext.Users.Where(x => x.UserName == "Kaloqn").FirstOrDefault(),
                    TicketSubmitter = dbContext.Users.Where(x => x.UserName == "Manol").FirstOrDefault(),
                    TicketSubmitterId = dbContext.Users.Where(x => x.UserName == "Manol").FirstOrDefault().Id,
                    ProjectId = dbContext.Projects.Where(x => x.Title == "TheAncientMerch").FirstOrDefault().Id,
                    Project = dbContext.Projects.Where(x => x.Title == "TheAncientMerch").FirstOrDefault(),
                    TicketType = "BugsErrors",
                    Priority = "Medium",
                    Status = "InProgress",
                },

                new Ticket
                {
                    Title = "Edit file",
                    Description = "Add Edit file for sculptures",
                    AssignedDeveloperId = dbContext.Users.Where(x => x.UserName == "Kaloqn").FirstOrDefault().Id,
                    TicketSubmitterId = dbContext.Users.Where(x => x.UserName == "Manol").FirstOrDefault().Id,
                    AssignedDeveloper = dbContext.Users.Where(x => x.UserName == "Kaloqn").FirstOrDefault(),
                    TicketSubmitter = dbContext.Users.Where(x => x.UserName == "Manol").FirstOrDefault(),
                    Project = dbContext.Projects.Where(x => x.Title == "TheAncientMerch").FirstOrDefault(),
                    ProjectId = dbContext.Projects.Where(x => x.Title == "TheAncientMerch").FirstOrDefault().Id,
                    TicketType = "FeatureRequests",
                    Priority = "Low",
                    Status = "Resolved",
                },

                new Ticket
                {
                    Title = "Edit views",
                    Description = "Make so on Edit pages the form model is fulfiled.",
                    AssignedDeveloperId = dbContext.Users.Where(x => x.UserName == "User").FirstOrDefault().Id,
                    TicketSubmitterId = dbContext.Users.Where(x => x.UserName == "Ivan").FirstOrDefault().Id,
                    ProjectId = dbContext.Projects.Where(x => x.Title == "BugTrackerSU").FirstOrDefault().Id,
                    AssignedDeveloper = dbContext.Users.Where(x => x.UserName == "User").FirstOrDefault(),
                    TicketSubmitter = dbContext.Users.Where(x => x.UserName == "Ivan").FirstOrDefault(),
                    Project = dbContext.Projects.Where(x => x.Title == "BugTrackerSU").FirstOrDefault(),
                    TicketType = "FeatureRequests",
                    Priority = "Medium",
                    Status = "InProgress",
                },

                new Ticket
                {
                    Title = "Document",
                    Description = "Add training documents for all employes.",
                    AssignedDeveloperId = dbContext.Users.Where(x => x.UserName == "User").FirstOrDefault().Id,
                    TicketSubmitterId = dbContext.Users.Where(x => x.UserName == "Manol").FirstOrDefault().Id,
                    ProjectId = dbContext.Projects.Where(x => x.Title == "LearnPlanProfit").FirstOrDefault().Id,
                    AssignedDeveloper = dbContext.Users.Where(x => x.UserName == "User").FirstOrDefault(),
                    TicketSubmitter = dbContext.Users.Where(x => x.UserName == "Manol").FirstOrDefault(),
                    Project = dbContext.Projects.Where(x => x.Title == "LearnPlanProfit").FirstOrDefault(),
                    TicketType = "TrainingDocumentRequests",
                    Priority = "High",
                    Status = "InProgress",
                },

                new Ticket
                {
                    Title = "Old documents",
                    Description = "Find and sort all old employees training documents.",
                    AssignedDeveloperId = dbContext.Users.Where(x => x.UserName == "User").FirstOrDefault().Id,
                    TicketSubmitterId = dbContext.Users.Where(x => x.UserName == "Manol").FirstOrDefault().Id,
                    ProjectId = dbContext.Projects.Where(x => x.Title == "LearnPlanProfit").FirstOrDefault().Id,
                    AssignedDeveloper = dbContext.Users.Where(x => x.UserName == "User").FirstOrDefault(),
                    TicketSubmitter = dbContext.Users.Where(x => x.UserName == "Manol").FirstOrDefault(),
                    Project = dbContext.Projects.Where(x => x.Title == "LearnPlanProfit").FirstOrDefault(),
                    TicketType = "TrainingDocumentRequests",
                    Priority = "None",
                    Status = "InProgress",
                },

                new Ticket
                {
                    Title = "Missing information",
                    Description = "Try and search all deleted infomation fro user #22231",
                    AssignedDeveloperId = dbContext.Users.Where(x => x.UserName == "Kaloqn").FirstOrDefault().Id,
                    TicketSubmitterId = dbContext.Users.Where(x => x.UserName == "Ivan").FirstOrDefault().Id,
                    ProjectId = dbContext.Projects.Where(x => x.Title == "MonsterCat").FirstOrDefault().Id,
                    AssignedDeveloper = dbContext.Users.Where(x => x.UserName == "Kaloqn").FirstOrDefault(),
                    TicketSubmitter = dbContext.Users.Where(x => x.UserName == "Ivan").FirstOrDefault(),
                    Project = dbContext.Projects.Where(x => x.Title == "MonsterCat").FirstOrDefault(),
                    TicketType = "OtherComments",
                    Priority = "Low",
                    Status = "InProgress",
                },
            };

            foreach (var ticket in tickets)
            {
                await dbContext.Tickets.AddAsync(new Ticket
                {
                    Title = ticket.Title,
                    Description = ticket.Description,
                    AssignedDeveloper = ticket.AssignedDeveloper,
                    TicketSubmitter = ticket.TicketSubmitter,
                    Project = ticket.Project,
                    AssignedDeveloperId = ticket.AssignedDeveloperId,
                    TicketSubmitterId = ticket.TicketSubmitterId,
                    ProjectId = ticket.ProjectId,
                    TicketType = ticket.TicketType,
                    Priority = ticket.Priority,
                    Status = ticket.Status,
                });
            }

        }
    }
}
