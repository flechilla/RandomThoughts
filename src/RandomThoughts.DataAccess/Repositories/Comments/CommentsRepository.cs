using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.Domain.Enums;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace RandomThoughts.DataAccess.Repositories.Comments
{
    class CommentsRepository : BaseRepository<RandomThoughts.Domain.Comment, int>, ICommentsRepository
    {
        private SqlConnection sqlConnection;
        public CommentsRepository(RandomThoughtsDbContext dbContext) : base(dbContext)
        {

        }
        /// <summary>
        /// method for get all comments related with discriminator
        /// TODO: use Dapper
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IQueryable<Domain.Comment> ReadAll((int idparent, Discriminator discriminator) filter)
        {

            using (var IDbConnection = new SqlConnection(ConnectionStringName))
            {
                sqlConnection.Open();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ParentId", filter.idparent);
                dynamicParameters.Add("@discriminator", filter.discriminator);
                var result = sqlConnection.Query("SELECT * FROM Comments WHERE @ParentId = ParentId AND @discriminator = ParentDiscriminator",dynamicParameters);
            }
            return this.Entities.Where(ent => ent.Id == filter.idparent && ent.ParentDiscriminator == filter.discriminator);            
        }

        public async Task<IQueryable<Domain.Comment>> ReadAllAsync((int idparent, Discriminator discriminator) filter)
        {
            using (var IDbConnection = new SqlConnection(ConnectionStringName))
            {
                sqlConnection.Open();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ParentId", filter.idparent);
                dynamicParameters.Add("@discriminator", filter.discriminator);
                var result = sqlConnection.Query("SELECT * FROM Comments WHERE @ParentId = ParentId AND @discriminator = ParentDiscriminator", dynamicParameters);
            }
            return await Task.Factory.StartNew(() =>
            {
                return this.Entities.Where(ent => ent.ParentId == filter.idparent && ent.ParentDiscriminator == filter.discriminator);
            });
        }
    }
}
