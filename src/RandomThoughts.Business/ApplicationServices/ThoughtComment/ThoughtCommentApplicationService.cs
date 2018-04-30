using RandomThoughts.Business.ApplicationServices.Comments;
using RandomThoughts.Business.Base;
using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.DataAccess.Repositories.Comments;
using RandomThoughts.Domain;
using System.Linq;
using System.Threading.Tasks;
using RandomThoughts.Domain.Enums;

namespace RandomThoughts.Business.ApplicationServices.ThoughtComment
{
    public class ThoughtCommentApplicationService : BaseApplicationService<RandomThoughts.Domain.Comments,int>, IThoughtCommentApplicationService
    {
        public ThoughtCommentApplicationService(ICommentsRepository repository) : base(repository)
        {
        }

        public IQueryable<Domain.Comments> ReadAll((int idparent, int discriminator) filter)
        {
            var repository = (Repository as RandomThoughts.DataAccess.Repositories.Comments.ICommentsRepository);
            return repository.ReadAll((filter.idparent, Discriminator.Thought));
        }

        public async Task<IQueryable<Domain.Comments>> ReadAllAsync((int idparent, int discriminator) filter)
        {
            var repository = (Repository as RandomThoughts.DataAccess.Repositories.Comments.ICommentsRepository);
            return await repository.ReadAllAsync((filter.idparent, Discriminator.Thought));
        }
    }
}
