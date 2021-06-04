using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DragonLegion.Models;
using Microsoft.AspNetCore.Identity;

namespace DragonLegion.Data
{
    public class DbDataInitializer
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;

        public DbDataInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public void InitializeData()
        {
            dbContext.Database.EnsureDeleted();
            if (dbContext.Database.EnsureCreated())
            {
                CreateUsers().Wait();

                var user = dbContext.Users.FirstOrDefault(u => u.UserName == "Mustachio");
            
                var posts = new List<Post>();
                for (int i = 0; i < 50; i++)
                {
                    var post = new Post()
                    {
                        Author = user,
                        Content = string.Concat(Enumerable.Repeat($"Test {i} *emphasis* # Title", 20)),
                        Subtitle = $"Test {i} Subtitle",
                        Title = $"Test {i} Title"
                    };
                    posts.Add(post);
                }

                var event1 = new Event()
                {
                    Title = "Event 1",
                    Description = "Description for event 1",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(5),
                    Location = "Location for event 1"
                };
                
                var event2 = new Event()
                {
                    Title = "Event 2",
                    Description = "Description for event 2",
                    StartDate = DateTime.Today.AddMonths(1),
                    EndDate = DateTime.Today.AddMonths(1).AddDays(6),
                    Location = "Location for event 2"
                };
            
                dbContext.Posts.AddRange(posts);
                dbContext.Events.Add(event1);
                dbContext.Events.Add(event2);
                dbContext.SaveChanges();
            }
        }

        public async Task CreateUsers()
        {
            var user = new IdentityUser()
            {
                UserName = "Mustachio",
                Email = "nicolas.van.damme2@hotmail.com"
            };
            user.EmailConfirmed = true;
            await userManager.CreateAsync(user, "P@ssword1");
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));
        }
    }
}
