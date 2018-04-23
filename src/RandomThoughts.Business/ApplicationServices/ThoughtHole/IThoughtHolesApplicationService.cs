using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.Business.Base;
using RandomThoughts.DataAccess.Repositories.ThoughtHoles;
using RandomThoughts.Domain;

namespace RandomThoughts.Business.ApplicationServices.ThoughtHole
{
    /// <summary>
    ///     <para>
    ///         Contains the declaration of the  necessary functionalities
    ///         to handle the operations on the <see cref="Domain.ThoughtHole" /> entity.
    ///     </para>
    ///     <remarks>
    ///         This object handle the data of the <see cref="Domain.ThoughtHole" /> entity
    ///         through the <see cref="IThoughtHolesRepository" /> but when necessary
    ///         adda some data or apply operations on the data before pass it to the DataAcces layer
    ///         or to the Presentation layer
    ///     </remarks>
    /// </summary>
    public interface IThoughtHolesApplicationService : IBaseApplicationService<Domain.ThoughtHole, int>
    {
    }
}
