using RandomThoughts.Business.Base;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.Domain.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace RandomThoughts.Business.ApplicationServices.Comments
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
        public CommentsApplicationService(RandomThoughts.DataAccess.Repositories.Comments.ICommentsRepository repository) : base(repository)
        {
        }

        public IQueryable<Domain.Comments> ReadAll((int idparent, Discriminator discriminator) filter)
        {
            var repository = (Repository as RandomThoughts.DataAccess.Repositories.Comments.ICommentsRepository);
            return repository.ReadAll((filter.idparent, filter.discriminator));
        }

        public async Task<IQueryable<Domain.Comments>> ReadAllAsync((int idparent, Discriminator discriminator) filter)
        {
            var repository = (Repository as RandomThoughts.DataAccess.Repositories.Comments.ICommentsRepository);

            return await repository.ReadAllAsync((filter.idparent, filter.discriminator));
        }
    }
}
