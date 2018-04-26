using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLipsum.Core;
using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.Domain;
using RandomThoughts.Domain.Enums;
using SeedEngine;

namespace RandomThoughts.DataAccess.Seeds
{
    public class ThoughtSeeds : ISeed<RandomThoughtsDbContext>
    {
        public int OrderToByApplied => 3;

        public void AddOrUpdate(RandomThoughtsDbContext context, int amountOfObjects = 20)
        {
            if (context.Thoughts.Any())
                return;

            #region Check for the parent dependencies of the object that we are gonna seed

            if (!context.ThoughtHoles.Any())
                throw new Exception("There have to be at leat one ThoughtHole in the DB before running this Seeds.");
            if (!context.Users.Any())
                throw new Exception("There have to be at leat one User in the DB before running this Seeds.");

            #endregion

            var thoughtHoles = context.ThoughtHoles.ToArray();
            var thoughts = new List<Thought>(amountOfObjects);
            var lipsumGen = new LipsumGenerator();

            var user = context.Users.First();
            var moods = Enum.GetValues(typeof(ThinkerMood));

            foreach (var thoughtHole in thoughtHoles)
                for (var i = 0; i < amountOfObjects; i++)
                {
                    var newThought = new Thought
                    {
                        Title = lipsumGen.GenerateSentences(1)[0],
                        Body = stringArrayFlatter(
                            lipsumGen.GenerateParagraphs(new Random(DateTime.UtcNow.Millisecond).Next(1, 10))),
                        ThoughtHole = thoughtHole,
                        ApplicationUserId = user.Id,
                        Likes = new Random(DateTime.UtcNow.Millisecond).Next(5, 1000),
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow,
                        Views = new Random(DateTime.UtcNow.Millisecond).Next(5, 1000000),
                        Mood = (ThinkerMood) moods.GetValue(
                            new Random(DateTime.UtcNow.Millisecond.GetHashCode()).Next(0, moods.Length))
                    };

                    thoughts.Add(newThought);
                }

            context.Thoughts.AddRange(thoughts);
            context.SaveChanges();
        }

        private static string stringArrayFlatter(string[] paragraphs)
        {
            var output = new StringBuilder();

            foreach (var p in paragraphs)
                output.Append(p);

            return output.ToString();
        }
    }
}