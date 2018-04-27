using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;

namespace SeedEngine
{
    public static class ApplySeeds
    {
        public static void EnsureSeedData<T>(this IApplicationBuilder app) where T: DbContext
        {
            var context = app.ApplicationServices.GetService(typeof(T)) as T;

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Log.Debug("Starting the seeding of the objects...");

            var contextAssambly = typeof(T).Assembly;

            if(!context.AllMigrationsApplied())
                throw new Exception("The migrations must be applied in order to run the Seeds.");

            var seedInstances = contextAssambly.GetTypes()
                .Where(type => type.GetInterfaces().Any(t => t == typeof(ISeed))).
                Select(t=>(t, Activator.CreateInstance(t)));

            Log.Debug($"Founded {seedInstances.Count()} seed classes.");

            var orderedSeeds = seedInstances.OrderBy<(Type, object), int>(tuple =>
            {
                var order = (int)tuple.Item1.GetProperty("OrderToByApplied").GetValue(tuple.Item2, null);
                return order;
            });

            foreach (var seedTuple in orderedSeeds)
            {
                try
                {
                    seedTuple.Item1.GetMethod("AddOrUpdate")
                        .Invoke(seedTuple.Item2, new List<object>() {context, 100}
                            .ToArray());
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Exception seeding in the seed class of name: {seedTuple.Item1.FullName}");
                }
            }
            stopWatch.Stop();
            Log.Debug($"Finished the seeding proccess after {stopWatch.Elapsed.Seconds}");
            context.Dispose();
        }
    }
}
