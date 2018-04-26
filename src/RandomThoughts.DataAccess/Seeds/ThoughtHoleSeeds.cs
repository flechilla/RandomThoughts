using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.Domain;
using SeedEngine;
using NLipsum.Core;

namespace RandomThoughts.DataAccess.Seeds
{
    /// <inheritdoc />
    /// Contains the implementation of the <see cref="ThoughtHole"/> objects
    public class ThoughtHoleSeeds : ISeed<RandomThoughtsDbContext>
    {
        public static void AddOrUpdate(RandomThoughtsDbContext context, int amountOfObjects = 20)
        {
            var thoughtHoles = new List<ThoughtHole>(amountOfObjects);
            var lipsumGen = new NLipsum.Core.LipsumGenerator();

            for (int i = 0; i < amountOfObjects; i++)
            {
                var newHole = new ThoughtHole()
                {
                    Name = lipsumGen.GenerateSentences(1)[0],
                    Description = lipsumGen.GenerateLipsum(40),
                    Likes = new Random(DateTime.UtcNow.Millisecond).Next(5, 1000),
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    Views = new Random(DateTime.UtcNow.Millisecond).Next(5, 1000000)
                };

                thoughtHoles.Add(newHole);
            }

            context.ThoughtHoles.AddRange(thoughtHoles);
            context.SaveChanges();
        }

        void ISeed<RandomThoughtsDbContext>.AddOrUpdate(RandomThoughtsDbContext context, int amountOfObjects)
        {
            AddOrUpdate(context, amountOfObjects);
        }
    }
}
