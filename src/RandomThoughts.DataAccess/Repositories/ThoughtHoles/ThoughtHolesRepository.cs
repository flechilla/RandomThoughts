using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;

namespace RandomThoughts.DataAccess.Repositories.ThoughtHoles
{
    public class ThoughtHolesRepository : BaseRepository<ThoughtHole, int>, IThoughtHolesRepository
    {
        public ThoughtHolesRepository(RandomThoughtsDbContext dbContext) : base(dbContext)
        {
        }
    }
}
