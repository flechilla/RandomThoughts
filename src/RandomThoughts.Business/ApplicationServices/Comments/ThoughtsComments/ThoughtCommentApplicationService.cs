using RandomThoughts.Business.Base;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;
using RandomThoughts.DataAccess.Repositories.Comments.ThoughtsComments;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomThoughts.Business.ApplicationServices.Comments.ThoughtsComments
{
    public class ThoughtCommentApplicationService : BaseApplicationService<Comment<Thought, int>, int>, IThoughtsCommentsApplicationService
    {
        /// <summary>
        /// <para>
        ///     Contains the implementation of the  necessary functionalities
        ///     to handle the operations on the <see cref="Comment{TEntity, TKey}"/> entity.
        /// </para>
        /// <remarks>
        ///     This object handle the data of the <see cref="Comment{TEntity, TKey}"/> entity
        ///     through the <see cref="IThoughtsCommentsRepository"/> but when necessary
        ///     add some operations on the data before pass it to the DataAcces layer
        ///     or to the Presentation layer
        /// </remarks>
        /// </summary>
        public ThoughtCommentApplicationService(IBaseRepository<Comment<Thought, int>, int> repository) : base(repository)
        {
        }
    }
}
