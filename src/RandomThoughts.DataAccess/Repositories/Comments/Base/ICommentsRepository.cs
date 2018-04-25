using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;
using RandomThoughts.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomThoughts.DataAccess.Repositories.Comments
{
    /// <summary>
    ///     Declares the functionalities for the 
    ///     operations on the <see cref="Comment{TEntity, TKey}"/> entity.
    /// </summary>
    public interface ICommentsRepository<TEntity, TKey> : IBaseRepository<Comment<TEntity, TKey>, int> where TEntity:IEntity<TKey>
    {
    }
}
