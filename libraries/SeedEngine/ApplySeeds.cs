using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace SeedEngine
{
    public static class ApplySeeds
    {
        public static void EnsureSeedData<T>(this IApplicationBuilder app, T context) where T: DbContext
        {
            var contextAssambly = typeof(T).Assembly;

            if(!context.AllMigrationsApplied())
                throw new Exception("The migrations must be applied in order to run the Seeds.");

            var seedInstances = contextAssambly.GetTypes()
                .Where(type => type.GetInterfaces().Any(t => t == typeof(ISeed))).
                Select(t=>(t, Activator.CreateInstance(t)));

            var orderedSeeds = seedInstances.OrderBy<(Type, object), int>(tuple =>
            {
                var order = (int)tuple.Item1.GetProperty("OrderToByApplied").GetValue(tuple.Item2, null);
                return order;
            });

            foreach (var seedTuple in orderedSeeds)
            {
                seedTuple.Item1.GetMethod("AddOrUpdate")
                    .Invoke(seedTuple.Item2, new List<object>() { context, 100 }
                        .ToArray());
                context.SaveChanges();
            }

            context.Dispose();
        }
    }
}
