using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RandomThoughts.Business.Base;
using RandomThoughts.DataAccess.Contexts;
using RandomThoughts.Domain.Base;

namespace RandomThoughts.Test.Common.Abstract
{
    /// <summary>
    ///     Declares the common member for all the unit Tests 
    ///     of the Business layer
    /// </summary>
    /// <typeparam name="TApplicationService">The type of the <see cref="IBaseApplicationService{TEntity,TKey}"/>
    /// to the tested.</typeparam>
    /// <typeparam name="TEntity">The type of entity for manipulate</typeparam>
    /// <typeparam name="TKey">The type of the key for that entity</typeparam>
    public interface IBaseApplicationServiceTest<out TApplicationService, TEntity, TKey>
        where TApplicationService : IBaseApplicationService<TEntity, TKey>
        where TEntity : Entity<TKey>
    {
        /// <summary>
        ///     Create an instance of the actual implementation of the interface
        ///      <value>TApplicationService</value> for testing its operations
        /// </summary>
        /// <param name="context">The DBcontext for be used in the unit tests</param>
        /// <returns>An instance of the actual implementation of the interface</returns>
        TApplicationService GetInstance(RandomThoughtsDbContext context);
    }
}
