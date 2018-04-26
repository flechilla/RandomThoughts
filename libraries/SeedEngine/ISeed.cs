using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SeedEngine
{
    /// <summary>
    ///     Declares the functionalities and objects that must be
    ///     implemented by the seed classes.
    /// </summary>
    public interface ISeed<in TContext> where TContext : DbContext
    {
        /// <summary>
        ///     Main method to run the seeds. This MUST contains the 
        ///     implementation of the seeds or calls to other methods
        ///     that implement the seeds for a given object type.
        /// </summary>
        /// <param name="context">
        ///     The <see cref="DbContext"/> of the application. Will be use
        ///     to access the <see cref="DbSet{TEntity}"/> of the entity
        ///     to be seeded.
        /// </param>
        /// <param name="amountOfObjects">
        ///     The amount of objects to be added. This should be used as the
        ///     upper bound of the cycle that creates and adds the new objects.
        /// </param>
        void AddOrUpdate(TContext context, int amountOfObjects = 20);
    }
}
