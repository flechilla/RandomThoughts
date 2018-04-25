using RandomThoughts.Business.Base;
using RandomThoughts.Domain;
using RandomThoughts.Domain.Base;
using RandomThoughts.DataAccess.Repositories.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomThoughts.Business.ApplicationServices.Comments.Base
{
    /// <summary>
    ///     <para>
    ///         Contains the declaration of the  necessary functionalities
    ///         to handle the operations on the <see cref="Comment{TEntity, TKey}" /> entity.
    ///     </para>
    ///     <remarks>
    ///         This object handle the data of the <see cref="Comment{TEntity, TKey}" /> entity
    ///         through the <see cref="ICommentsRepository{TEntity, TKey}" /> but when necessary
    ///         adda some data or apply operations on the data before pass it to the DataAcces layer
    ///         or to the Presentation layer
    ///     </remarks>
    /// </summary>
    /// <typeparam name="TEntity">The type of entity for manipulate</typeparam>
    /// <typeparam name="TKey">The type of the key for that entity</typeparam>
    public interface ICommentApplicationService<TEntity, TKey> : IBaseApplicationService<Comment<TEntity, TKey>, int> where TEntity:IEntity<TKey>
    {
    }
}
