using RandomThoughts.Business.Base;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomThoughts.Business.ApplicationServices.Comments.ThoughtsComments
{
    public class CommentsApplicationService : BaseApplicationService<RandomThoughts.Domain.Comments, int>, ICommentsApplicationService
    {
        /// <summary>
        /// <para>
        ///     Contains the implementation of the  necessary functionalities
        ///     to handle the operations on the <see cref="RandomThoughts.Domain.Comments"/> entity.
        /// </para>
        /// <remarks>
        ///     This object handle the data of the <see cref="RandomThoughts.Domain.Comments"/> entity
        ///     through the <see cref="ICommentsRepository"/> but when necessary
        ///     add some operations on the data before pass it to the DataAcces layer
        ///     or to the Presentation layer
        /// </remarks>
        /// </summary>
        public CommentsApplicationService(IBaseRepository<RandomThoughts.Domain.Comments, int> repository) : base(repository)
        {
        }
    }
}
