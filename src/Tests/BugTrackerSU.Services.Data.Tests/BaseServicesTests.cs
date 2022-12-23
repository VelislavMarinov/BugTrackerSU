namespace BugTrackerSU.Services.Data.Tests
{
    using System;

    using BugTrackerSU.Data;
    using Microsoft.EntityFrameworkCore;

    public class BaseServicesTests
    {
        public static ApplicationDbContext GetDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);

            return db;
        }
    }
}
