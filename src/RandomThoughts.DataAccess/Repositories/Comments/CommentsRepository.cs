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
        public CommentsRepository(RandomThoughtsDbContext dbContext) : base(dbContext)
        {

        }
        /// <summary>
        /// method for get all comments related with discriminator
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IQueryable<Comment> ReadAll((int idparent, Discriminator discriminator) filter)
        {

            try
            {
                var conn = DbConnection.ConnectionString;
                using (var IDbConnection = new SqlConnection(conn))
                {
                    IDbConnection.Open();
                    var result = IDbConnection.Query("SELECT * FROM Comments WHERE ParentId = @ParentId AND ParentDiscriminator = @discriminator ", new { discriminator = filter.discriminator, ParentId = filter.idparent });
                    IDbConnection.Close();
                    //TODO: Solve problem
                    return (result as IQueryable<Comment>);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// get comments for given discriminator and parentId, and take Count
        /// </summary>
        /// <param name="filter">Filters for Search in Comments</param>
        /// <param name="count">Number of Comment to take</param>
        /// <returns></returns>
        public IQueryable<Comment> ReadAll((int idparent, Discriminator discriminator) filter, int count)
        {
            try
            {
                var conn = DbConnection.ConnectionString;
                using (var IDbConnection = new SqlConnection(conn))
                {
                    IDbConnection.Open();
                    var result = IDbConnection.Query("SELECT TOP(@count) * FROM Comments WHERE ParentId = @ParentId AND ParentDiscriminator = @discriminator ", new {count = count, discriminator = filter.discriminator, ParentId = filter.idparent });
                    IDbConnection.Close();
                    //TODO: Solve problem
                    return (result as IQueryable<Comment>);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IQueryable<Comment>> ReadAllAsync((int idparent, Discriminator discriminator) filter)
        {
            try
            {
                var conn = DbConnection.ConnectionString;
                using (var IDbConnection = new SqlConnection(conn))
                {
                    IDbConnection.Open();
                    var result = IDbConnection.Query("SELECT * FROM Comments WHERE ParentId = @ParentId AND ParentDiscriminator = @discriminator ", new { discriminator = filter.discriminator, ParentId = filter.idparent });
                    IDbConnection.Close();
                    return await Task.Factory.StartNew(() =>
                    {
                        //TODO: Solve problem
                        return result as IQueryable<Comment>;
                    });
                }
            }
            catch (Exception e)
            {
                return null;
            }           
        }

        public async Task<IQueryable<Comment>> ReadAllAsync((int idparent, Discriminator discriminator) filter, int count)
        {
            try
            {
                var conn = DbConnection.ConnectionString;
                using (var IDbConnection = new SqlConnection(conn))
                {
                    IDbConnection.Open();
                    var result = IDbConnection.Query("SELECT TOP(@count) * FROM Comments WHERE ParentId = @ParentId AND ParentDiscriminator = @discriminator ", new {count = count, discriminator = filter.discriminator, ParentId = filter.idparent });
                    IDbConnection.Close();
                    return await Task.Factory.StartNew(() =>
                    {
                        //TODO: Solve problem
                        return result as IQueryable<Comment>;
                    });
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
