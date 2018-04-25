using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomThoughts.DataAccess.Repositories.Comments.ThoughtsComments
{
    class ThoughtsCommentsRepository : BaseRepository<Comment<Thought, int>, int>, IThoughtsCommentsRepository
    {
        public ThoughtsCommentsRepository(RandomThoughtsDbContext dbContext) : base(dbContext)
        {

        }
    }
}
