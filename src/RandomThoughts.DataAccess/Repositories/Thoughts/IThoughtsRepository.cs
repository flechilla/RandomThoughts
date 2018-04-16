using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;

namespace RandomThoughts.DataAccess.Repositories.Thoughts
{
    /// <summary>
    ///     Declares the functionalities for the 
    ///     operations on the <see cref="Thought"/> entity.
    /// </summary>
    public interface IThoughtsRepository : IBaseRepository<Thought, int>
    {
    }
}
