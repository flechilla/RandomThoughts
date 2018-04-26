using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomThoughts.DataAccess.Repositories.Comments
{
    class CommentsRepository : BaseRepository<RandomThoughts.Domain.Comments, int>, ICommentsRepository
    {
        public CommentsRepository(RandomThoughtsDbContext dbContext) : base(dbContext)
        {

        }
    }
}
