using RandomThoughts.Business.Base;
using RandomThoughts.Domain;
using RandomThoughts.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomThoughts.Business.ApplicationServices.Comments
{
    /// <summary>
    ///     <para>
    ///         Contains the declaration of the  necessary functionalities
    ///         to handle the operations on the <see cref="RandomThoughts.Domain.Comment" /> entity.
    ///     </para>
    ///     <remarks>
    ///         This object handle the data of the <see cref="RandomThoughts.Domain.Comment" /> entity
    ///         through the <see cref="ICommentsRepository" /> but when necessary
    ///         adda some data or apply operations on the data before pass it to the DataAcces layer
    ///         or to the Presentation layer
    ///     </remarks>
    /// </summary>
    public interface ICommentsApplicationService : IBaseApplicationService<RandomThoughts.Domain.Comment,int>
    {
        #region Sync Members
        /// <summary>
        ///     Filter the comments in the table based on
        ///     the given parentID and Discriminator
        /// </summary>
        IQueryable<RandomThoughts.Domain.Comment> ReadAll(int idparent);

        void AddComment(RandomThoughts.Domain.Comment comments);
        #endregion

        #region Async Members
        /// <summary>
        ///     Asynchronously filter the elements in the table based on
        ///     the given predicate
        /// </summary>
        /// <param name="filter">A function to be applied in each element of the table</param>
        /// <returns>The elements that satisfy the predicate <paramref name="filter"/></returns>
        Task<IQueryable<RandomThoughts.Domain.Comment>> ReadAllAsync(int idparent);
        #endregion
    }
}
