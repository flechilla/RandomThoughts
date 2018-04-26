using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.Domain;
using SeedEngine;
using Microsoft.AspNetCore.Identity;

namespace RandomThoughts.DataAccess.Seeds
{
    /// <inheritdoc />
    /// Contains the implementation of the seeds for 
    /// the <see cref="ApplicationUser"/>
    public class UserSeeds : ISeed<RandomThoughtsDbContext>
    {
        public void AddOrUpdate(RandomThoughtsDbContext context, int amountOfObjects = 20)
        {
            var mainUser = new ApplicationUser()
            {
                Email = "admin@gmail.com",
                EmailConfirmed = true, 
                NickName = "admin",
                UserName = "admin"
            };

            var passHasher = new PasswordHasher<ApplicationUser>();

            var hashedPass = passHasher.HashPassword(mainUser, "admin123");

            mainUser.PasswordHash = hashedPass;

            context.Users.Add(mainUser);

            context.SaveChanges();
        }
    }
}
