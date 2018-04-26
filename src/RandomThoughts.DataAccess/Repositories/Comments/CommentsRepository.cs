using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.Domain.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace RandomThoughts.DataAccess.Repositories.Comments
{
    class CommentsRepository : BaseRepository<RandomThoughts.Domain.Comments, int>, ICommentsRepository
    {
        public CommentsRepository(RandomThoughtsDbContext dbContext) : base(dbContext)
        {

        }
        /// <summary>
        /// method for get all comments related with discriminator
        /// TODO: use Dapper
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IQueryable<Domain.Comments> ReadAll((int idparent, Discriminator discriminator) filter)
        {
            return this.Entities.Where(ent => ent.Id == filter.idparent && ent.ParentDiscriminator == filter.discriminator);            
        }

        public async Task<IQueryable<Domain.Comments>> ReadAllAsync((int idparent, Discriminator discriminator) filter)
        {
            return await Task.Factory.StartNew(() =>
            {
                return this.Entities.Where(ent => ent.ParentId == filter.idparent && ent.ParentDiscriminator == filter.discriminator);
            });
        }
    }
}
