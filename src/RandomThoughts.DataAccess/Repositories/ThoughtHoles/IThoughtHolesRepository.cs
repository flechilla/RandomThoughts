using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;

namespace RandomThoughts.DataAccess.Repositories.ThoughtHoles
{
    /// <summary>
    ///     Declares the functionalities for the dataAccess layer
    ///     for the <see cref="ThoughtHole"/> entity.
    /// </summary>
    public interface IThoughtHolesRepository : IBaseRepository<ThoughtHole, int>
    {
    }
}
