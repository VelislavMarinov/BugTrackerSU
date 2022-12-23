namespace BugTrackerSU.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BugTrackerSU.Common;
    using BugTrackerSU.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Users.Any(x => x.UserName == "user"))
            {
                var user = new ApplicationUser
                {
                    AccessFailedCount = 0,
                    Email = "user@mail.com",
                    NormalizedEmail = "USER@MAIL.COM",
                    TwoFactorEnabled = false,
                    EmailConfirmed = false,
                    IsDeleted = false,
                    CreatedOn = DateTime.UtcNow,
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = true,
                    UserName = "User",
                    NormalizedUserName = "USER",
                    PasswordHash = "AQAAAAEAACcQAAAAEGQ9IfdyJPYzY9lKPmwWrdN6T3AbPWBjEYxlLW7yfiOGd/4w/wqPv2Q+5O11ncA0gQ==", // Password = 123456
                };

                user.Roles.Add(new IdentityUserRole<string>()
                {
                    RoleId = dbContext.Roles
                  .FirstOrDefault(r => r.Name == GlobalConstants.DeveloperRoleName)?.Id,
                });

                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Users.Any(x => x.UserName == "Nikola"))
            {
                var user = new ApplicationUser
                {
                    AccessFailedCount = 0,
                    Email = "nikola@mail.com",
                    NormalizedEmail = "NIKOLA@MAIL.COM",
                    TwoFactorEnabled = false,
                    EmailConfirmed = false,
                    IsDeleted = false,
                    CreatedOn = DateTime.UtcNow,
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = true,
                    UserName = "Nikola",
                    NormalizedUserName = "NIKOLA",
                    PasswordHash = "AQAAAAEAACcQAAAAEGQ9IfdyJPYzY9lKPmwWrdN6T3AbPWBjEYxlLW7yfiOGd/4w/wqPv2Q+5O11ncA0gQ==", // Password = 123456
                };

                user.Roles.Add(new IdentityUserRole<string>()
                {
                    RoleId = dbContext.Roles
                  .FirstOrDefault(r => r.Name == GlobalConstants.ProjectManagerRoleName)?.Id,
                });

                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Users.Any(x => x.UserName == "Pesho"))
            {
                var user = new ApplicationUser
                {
                    AccessFailedCount = 0,
                    Email = "pesho@mail.com",
                    NormalizedEmail = "PESHO@MAIL.COM",
                    TwoFactorEnabled = false,
                    EmailConfirmed = false,
                    IsDeleted = false,
                    CreatedOn = DateTime.UtcNow,
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = true,
                    UserName = "Pesho",
                    NormalizedUserName = "PESHO",
                    PasswordHash = "AQAAAAEAACcQAAAAEGQ9IfdyJPYzY9lKPmwWrdN6T3AbPWBjEYxlLW7yfiOGd/4w/wqPv2Q+5O11ncA0gQ==", // Password = 123456
                };

                user.Roles.Add(new IdentityUserRole<string>()
                {
                    RoleId = dbContext.Roles
                  .FirstOrDefault(r => r.Name == GlobalConstants.ProjectManagerRoleName)?.Id,
                });

                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Users.Any(x => x.UserName == "Ivan"))
            {
                var user = new ApplicationUser
                {
                    AccessFailedCount = 0,
                    Email = "ivan@mail.com",
                    NormalizedEmail = "IVAN@MAIL.COM",
                    TwoFactorEnabled = false,
                    EmailConfirmed = false,
                    IsDeleted = false,
                    CreatedOn = DateTime.UtcNow,
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = true,
                    UserName = "Ivan",
                    NormalizedUserName = "IVAN",
                    PasswordHash = "AQAAAAEAACcQAAAAEGQ9IfdyJPYzY9lKPmwWrdN6T3AbPWBjEYxlLW7yfiOGd/4w/wqPv2Q+5O11ncA0gQ==", // Password = 123456
                };

                user.Roles.Add(new IdentityUserRole<string>()
                {
                    RoleId = dbContext.Roles
                  .FirstOrDefault(r => r.Name == GlobalConstants.SubmitterRoleName)?.Id,
                });

                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Users.Any(x => x.UserName == "Manol"))
            {
                var user = new ApplicationUser
                {
                    AccessFailedCount = 0,
                    Email = "manol@mail.com",
                    NormalizedEmail = "MANOL@MAIL.COM",
                    TwoFactorEnabled = false,
                    EmailConfirmed = false,
                    IsDeleted = false,
                    CreatedOn = DateTime.UtcNow,
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = true,
                    UserName = "Manol",
                    NormalizedUserName = "MANOL",
                    PasswordHash = "AQAAAAEAACcQAAAAEGQ9IfdyJPYzY9lKPmwWrdN6T3AbPWBjEYxlLW7yfiOGd/4w/wqPv2Q+5O11ncA0gQ==", // Password = 123456
                };

                user.Roles.Add(new IdentityUserRole<string>()
                {
                    RoleId = dbContext.Roles
                  .FirstOrDefault(r => r.Name == GlobalConstants.SubmitterRoleName)?.Id,
                });

                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Users.Any(x => x.UserName == "Kaloqn"))
            {
                var user = new ApplicationUser
                {
                    AccessFailedCount = 0,
                    Email = "Kala@mail.com",
                    NormalizedEmail = "KALA@MAIL.COM",
                    TwoFactorEnabled = false,
                    EmailConfirmed = false,
                    IsDeleted = false,
                    CreatedOn = DateTime.UtcNow,
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = true,
                    UserName = "Kaloqn",
                    NormalizedUserName = "KALOQN",
                    PasswordHash = "AQAAAAEAACcQAAAAEGQ9IfdyJPYzY9lKPmwWrdN6T3AbPWBjEYxlLW7yfiOGd/4w/wqPv2Q+5O11ncA0gQ==", // Password = 123456
                };

                user.Roles.Add(new IdentityUserRole<string>()
                {
                    RoleId = dbContext.Roles
                  .FirstOrDefault(r => r.Name == GlobalConstants.DeveloperRoleName)?.Id,
                });

                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Users.Any(x => x.UserName == "Qvor"))
            {
                var user = new ApplicationUser
                {
                    AccessFailedCount = 0,
                    Email = "Qvor@mail.com",
                    NormalizedEmail = "QVOR@MAIL.COM",
                    TwoFactorEnabled = false,
                    EmailConfirmed = false,
                    IsDeleted = false,
                    CreatedOn = DateTime.UtcNow,
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = true,
                    UserName = "Qvor",
                    NormalizedUserName = "QVOR",
                    PasswordHash = "AQAAAAEAACcQAAAAEGQ9IfdyJPYzY9lKPmwWrdN6T3AbPWBjEYxlLW7yfiOGd/4w/wqPv2Q+5O11ncA0gQ==", // Password = 123456
                };

                user.Roles.Add(new IdentityUserRole<string>()
                {
                    RoleId = dbContext.Roles
                  .FirstOrDefault(r => r.Name == GlobalConstants.DeveloperRoleName)?.Id,
                });

                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Users.Any(x => x.UserName == "Radi"))
            {
                var user = new ApplicationUser
                {
                    AccessFailedCount = 0,
                    Email = "Radi@mail.com",
                    NormalizedEmail = "RADI@MAIL.COM",
                    TwoFactorEnabled = false,
                    EmailConfirmed = false,
                    IsDeleted = false,
                    CreatedOn = DateTime.UtcNow,
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = true,
                    UserName = "Radi",
                    NormalizedUserName = "RADI",
                    PasswordHash = "AQAAAAEAACcQAAAAEGQ9IfdyJPYzY9lKPmwWrdN6T3AbPWBjEYxlLW7yfiOGd/4w/wqPv2Q+5O11ncA0gQ==", // Password = 123456
                };

                user.Roles.Add(new IdentityUserRole<string>()
                {
                    RoleId = dbContext.Roles
                  .FirstOrDefault(r => r.Name == GlobalConstants.DeveloperRoleName)?.Id,
                });

                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                return;
            }

            if (dbContext.Users.Any(u => u.Email == "admin@admin.com"))
            {
                return;
            }

            var admin = new ApplicationUser
            {
                AccessFailedCount = 0,
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                TwoFactorEnabled = false,
                EmailConfirmed = false,
                IsDeleted = false,
                CreatedOn = DateTime.UtcNow,
                LockoutEnabled = false,
                PhoneNumberConfirmed = true,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                PasswordHash = "AQAAAAEAACcQAAAAEGQ9IfdyJPYzY9lKPmwWrdN6T3AbPWBjEYxlLW7yfiOGd/4w/wqPv2Q+5O11ncA0gQ==", // Password = 123456
            };

            admin.Roles.Add(new IdentityUserRole<string>()
            {
                RoleId = dbContext.Roles
                    .FirstOrDefault(r => r.Name == GlobalConstants.AdministratorRoleName)?.Id,
            });

            await dbContext.Users.AddAsync(admin);
            await dbContext.SaveChangesAsync();
        }
    }
}
