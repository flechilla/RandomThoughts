using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;

namespace RandomThoughts.DataAccess.Contexts
{
    public class RandomThoughtsDbContext : IdentityDbContext, ISqlDbContext
    {
        public RandomThoughtsDbContext(DbContextOptions<RandomThoughtsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasMany(x => x.Thoughts).WithOne(x => x.ApplicationUser)
                .HasForeignKey(t => t.ApplicationUserId);

            builder.Entity<Thought>().Property(x => x.ApplicationUserId).IsRequired();

            builder.Entity<ThoughtComments>().Property(x => x.ApplicationUserId).IsRequired();

        }

        /// <summary>
        ///     Gets or sets the <see cref="Thought"/> of the platform.
        /// </summary>
        public DbSet<Thought> Thoughts { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="ThoughtHole"/> of the platform.
        /// </summary>
        public DbSet<ThoughtHole> ThoughtHoles { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="Comment"/> of the platform.
        /// </summary>
        public DbSet<ThoughtComments> Comments { get; set; }

    }
}
