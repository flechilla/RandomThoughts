using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SeedEngine
{
    /// <summary>
    ///     Contains functionalities that extends the
    ///     <see cref="DbContext"/>
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        ///     Checks if all the migrations are
        ///     applied to the context.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> to check</param>
        /// <returns>True if all the migrations are applied;otherwise, false/</returns>
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        /// <summary>
        ///     Mark as 'deleted' all Selected entities
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <param name="context">The <see cref="DbContext"/> to check</param>
        public static void DeleteAll<T>(this DbContext context)
            where T : class
        {
            foreach (var p in context.Set<T>())
            {
                context.Entry(p).State = EntityState.Deleted;
            }
        }
    }
}
