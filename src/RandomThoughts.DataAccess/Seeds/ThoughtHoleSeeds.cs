using System;
using System.Collections.Generic;
using System.Linq;
using NLipsum.Core;
using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.Domain;
using SeedEngine;
using SeedEngine.Core;

namespace RandomThoughts.DataAccess.Seeds
{
    /// <inheritdoc />
    /// Contains the implementation of the
    /// <see cref="ThoughtHole" /> objects
    public class ThoughtHoleSeeds : ISeed<RandomThoughtsDbContext>
    {
        ///<inheritdoc />
        public int OrderToByApplied => 2;

        public void AddOrUpdate(RandomThoughtsDbContext context, int amountOfObjects = 20)
        {
            if (context.ThoughtHoles.Any())
                return;

            var thoughtHoles = new List<ThoughtHole>(amountOfObjects);
            var lipsumGen = new LipsumGenerator();

            for (var i = 0; i < amountOfObjects; i++)
            {
                var newHole = new ThoughtHole
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
    }
}