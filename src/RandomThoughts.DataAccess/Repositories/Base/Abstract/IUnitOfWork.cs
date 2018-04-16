using RandomThoughts.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomThoughts.DataAccess.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves changes to the underlying store
        /// </summary>
        /// <returns>The number of afected objects</returns>
        int SaveChanges();

        /// <summary>
        /// Saves changes to the underlying store asynchronously
        /// </summary>
        /// <returns>The number of afected objects</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Provides the set of entities of <typeparamref name="TEntity"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities</typeparam>
        /// <typeparam name="TKey">The Key of the Entity.</typeparam>
        /// <returns>A repository of entities</returns>
        IBaseRepository<TEntity, TKey> Set<TEntity, TKey>()
            where TEntity : Entity<TKey>;

        /// <summary>
        /// Executes a raw query to the underlying store
        /// </summary>
        /// <typeparam name="TResult">The type of the entities returned by the query</typeparam>
        /// <param name="query">The query to execute</param>
        /// <param name="queryParams">The query parameters</param>
        /// <returns>A collection of elements returned by the query</returns>
        ICollection<TResult> RawQuery<TResult>(string query, object queryParams = null);

        /// <summary>
        /// Executes a raw query to the underlying store asynchronously
        /// </summary>
        /// <typeparam name="TResult">The type of the entities returned by the query</typeparam>
        /// <param name="query">The query to execute</param>
        /// <param name="queryParams">The query parameters</param>
        /// <returns>A collection of elements returned by the query</returns>
        Task<ICollection<TResult>> RawQueryAsync<TResult>(string query, object queryParams = null);
    }
}
