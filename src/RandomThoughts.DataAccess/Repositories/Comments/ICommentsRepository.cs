using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;

namespace RandomThoughts.DataAccess.Repositories.Comments
{
    /// <summary>
    ///     Declares the functionalities for the 
    ///     operations on the <see cref="RandomThoughts.Domain.Comments"/> entity.
    /// </summary>
    public interface ICommentsRepository : IBaseRepository<RandomThoughts.Domain.Comments, int>
    {
        
    }
}
