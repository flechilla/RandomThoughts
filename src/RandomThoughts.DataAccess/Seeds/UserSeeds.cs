using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.Domain;
using SeedEngine;

namespace RandomThoughts.DataAccess.Seeds
{
    /// <inheritdoc />
    /// Contains the implementation of the seeds for 
    /// the
    /// <see cref="ApplicationUser" />
    public class UserSeeds : ISeed<RandomThoughtsDbContext>
    {
        /// <inheritdoc />
        public int OrderToByApplied => 1;

        public void AddOrUpdate(RandomThoughtsDbContext context, int amountOfObjects = 20)
        {
            if (context.Users.Any())
                return;

            var mainUser = new ApplicationUser
            {
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                NickName = "admin",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var passHasher = new PasswordHasher<ApplicationUser>();

            var hashedPass = passHasher.HashPassword(mainUser, "admin123");

            mainUser.PasswordHash = hashedPass;

            context.Users.Add(mainUser);
            context.SaveChanges();
        }
    }
}