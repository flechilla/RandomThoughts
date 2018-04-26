using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;
using System.Linq;
using RandomThoughts.Domain.Enums;
using System.Threading.Tasks;

namespace RandomThoughts.DataAccess.Repositories.Comments
{
    /// <summary>
    ///     Declares the functionalities for the 
    ///     operations on the <see cref="RandomThoughts.Domain.Comments"/> entity.
    /// </summary>
    public interface ICommentsRepository : IBaseRepository<RandomThoughts.Domain.Comments, int>
    {
        #region Sync Members
        /// <summary>
        ///     Filter the comments in the table based on
        ///     the given parentID and Discriminator
        /// </summary>
        IQueryable<RandomThoughts.Domain.Comments> ReadAll((int idparent, Discriminator discriminator) filter);
        #endregion

        #region Async Members
        /// <summary>
        ///     Asynchronously filter the elements in the table based on
        ///     the given predicate
        /// </summary>
        /// <param name="filter">A function to be applied in each element of the table</param>
        /// <returns>The elements that satisfy the predicate <paramref name="filter"/></returns>
        Task<IQueryable<RandomThoughts.Domain.Comments>> ReadAllAsync((int idparent, Discriminator discriminator) filter);
        #endregion
    }
}
