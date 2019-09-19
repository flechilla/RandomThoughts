using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.Business.Base;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.DataAccess.Repositories.ThoughtHoles;

namespace RandomThoughts.Business.ApplicationServices.ThoughtHole
{
    public class ThoughtHolesApplicationService : BaseApplicationService<Domain.ThoughtHole, int>, IThoughtHolesApplicationService
    {
        /// <summary>
        /// <para>
        ///     Contains the implementation of the  necessary functionalities
        ///     to handle the operations on the <see cref="Domain.ThoughtHole"/> entity.
        /// </para>
        /// <remarks>
        ///     This object handle the data of the <see cref="Domain.ThoughtHole"/> entity
        ///     through the <see cref="IThoughtHolesRepository"/> but when necessary
        ///     add some operations on the data before pass it to the DataAcces layer
        ///     or to the Presentation layer
        /// </remarks>
        /// </summary>
        public ThoughtHolesApplicationService(IThoughtHolesRepository repository) : base(repository)
        {
        }
    }
}
