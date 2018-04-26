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

            var seedTypes = contextAssambly.GetTypes()
                .Where(type => type.GetInterfaces().Any(t => t == typeof(ISeed))).ToArray();

            var seed1 = seedTypes[2];

            var v3 = seed1.GetProperty("OrderToByApplied");


            var v2 = seed1.GetProperty("foo").GetValue(seed1, null);



            var orderedSeeds = seedTypes.OrderBy<Type, int>(t => (int)t.GetProperty("OrderToByApplied").GetValue(t));

            foreach (var seedType in orderedSeeds)
            {
                seedType.GetMethod("AddOrUpdate")
                    .Invoke(null, new List<object>() { context, 100 }
                        .ToArray());
                context.SaveChanges();
            }

            context.Dispose();
        }
    }
}
