using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;

namespace RandomThoughts.DataAccess.Repositories.Comments.ThoughtsComments
{
    /// <summary>
    ///     Declares the functionalities for the 
    ///     operations on the <see cref="Comment{TEntity, TKey}"/> entity.
    /// </summary>
    public interface IThoughtsCommentsRepository : ICommentsRepository<Thought, int>
    {

    }
}
