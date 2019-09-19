using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.Business.Base;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.DataAccess.Repositories.Thoughts;
using RandomThoughts.Domain;

namespace RandomThoughts.Business.ApplicationServices.Thoughts
{
    public class ThoughtsApplicationService : BaseApplicationService<Thought, int>, IThoughtsApplicationService
    {
        /// <summary>
        /// <para>
        ///     Contains the implementation of the  necessary functionalities
        ///     to handle the operations on the <see cref="Thought"/> entity.
        /// </para>
        /// <remarks>
        ///     This object handle the data of the <see cref="Thought"/> entity
        ///     through the <see cref="IThoughtsRepository"/> but when necessary
        ///     add some operations on the data before pass it to the DataAcces layer
        ///     or to the Presentation layer
        /// </remarks>
        /// </summary>
        public ThoughtsApplicationService(IThoughtsRepository repository) : base(repository)
        {
        }
    }
}
