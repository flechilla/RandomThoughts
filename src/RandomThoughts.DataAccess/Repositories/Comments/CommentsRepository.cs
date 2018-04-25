using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;
using RandomThoughts.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomThoughts.DataAccess.Repositories.Comments
{
    public class CommentsRepository<TEntity, TKey> : BaseRepository<Comment<TEntity, TKey>, int>,
                                                     ICommentsRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        public CommentsRepository(RandomThoughtsDbContext dbContext) : base(dbContext)
        {
        }
    }
}
