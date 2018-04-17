using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;

namespace RandomThoughts.DataAccess.Repositories.Thoughts
{
    public class ThoughtsRepository : BaseRepository<Thought, int>, IThoughtsRepository
    {
        public ThoughtsRepository(RandomThoughtsDbContext dbContext) : base(dbContext)
        {
        }
    }
}
