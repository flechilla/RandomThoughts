using RandomThoughts.Business.Base;
using RandomThoughts.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomThoughts.Business.ApplicationServices.Comments.ThoughtsComments
{
    /// <summary>
    ///     <para>
    ///         Contains the declaration of the  necessary functionalities
    ///         to handle the operations on the <see cref="Comment{TEntity, TKey}" /> entity.
    ///     </para>
    ///     <remarks>
    ///         This object handle the data of the <see cref="Comment{TEntity, TKey}" /> entity
    ///         through the <see cref="ICommentsRepository" /> but when necessary
    ///         adda some data or apply operations on the data before pass it to the DataAcces layer
    ///         or to the Presentation layer
    ///     </remarks>
    /// </summary>
    public interface ICommentsApplicationService : IBaseApplicationService<RandomThoughts.Domain.Comments,int>
    {
    }
}
