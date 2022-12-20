﻿namespace BugTrackerSU.Services.Data.MinorTask
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BugTrackerSU.Data.Common.Repositories;
    using BugTrackerSU.Data.Models;
    using BugTrackerSU.Web.ViewModels.MinorTasks;

    public class MinorTaskService : IMinorTaskService
    {
        private readonly IDeletableEntityRepository<MinorTask> minorTaskRepository;

        private readonly IDeletableEntityRepository<Ticket> ticketRepository;

        public MinorTaskService(
            IDeletableEntityRepository<MinorTask> minorTaskRepository,
            IDeletableEntityRepository<Ticket> ticketRepository)
        {
            this.minorTaskRepository = minorTaskRepository;
            this.ticketRepository = ticketRepository;
        }

        public bool ChekIfUserIsAuthorizedToCreateOrSeeTask(int ticketId, string userId, string role)
        {
            if (role == "Administrator")
            {
                return true;
            }

            var ticket = this.ticketRepository
                .All()
                .Where(x => x.Id == ticketId)
                .Where(x => x.TicketSubmitterId == userId
                || x.AssignedDeveloperId == userId
                || x.Project.ProjectManagerId == userId)
                .FirstOrDefault();

            if (ticket == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task CreateMinorTaskAsync(CreateMinorTaskFormModel model, string userId)
        {
            var task = new MinorTask
            {
                AddedByUserId = userId,
                TicketId = model.TicketId,
                Title = model.Title,
                Content = model.Content,
                Started = false,
                Finished = false,
                Type = model.TaskType.ToString(),
            };

            await this.minorTaskRepository.AddAsync(task);
            await this.minorTaskRepository.SaveChangesAsync();
        }

        public async Task FinishTask(int taskId)
        {
            var task = this.minorTaskRepository.All().Where(x => x.Id == taskId).FirstOrDefault();
            task.Finished = true;

            this.minorTaskRepository.Update(task);
            await this.minorTaskRepository.SaveChangesAsync();
        }

        public List<MinorTaskViewModel> GetTicketTasksById(int ticketId, int pageNumber, int itemsPerPage)
        {
            var model = this.minorTaskRepository
                .All()
                .Where(x => x.TicketId == ticketId)
                .OrderByDescending(x => x.Id)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new MinorTaskViewModel
                {
                    TaskType = x.Type,
                    Title = x.Title,
                    Content = x.Content,
                    Finished = x.Finished,
                    Started = x.Started,
                })
                .ToList();

            return model;
        }

        public int GetTicketTasksCount(int ticketId) => this.minorTaskRepository.All().Where(x => x.TicketId == ticketId).Count();

        public async Task StartTask(int taskId)
        {
            var task = this.minorTaskRepository.All().Where(x => x.Id == taskId).FirstOrDefault();
            task.Started = true;

            this.minorTaskRepository.Update(task);
            await this.minorTaskRepository.SaveChangesAsync();
        }
    }
}