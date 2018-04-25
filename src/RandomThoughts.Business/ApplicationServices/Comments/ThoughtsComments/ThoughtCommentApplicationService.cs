using RandomThoughts.Business.Base;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomThoughts.Business.ApplicationServices.Comments.ThoughtsComments
{
    public class ThoughtCommentApplicationService : BaseApplicationService<Comment<Thought, int>, int>, IThoughtsCommentsApplicationService
    {
        protected ThoughtCommentApplicationService(IBaseRepository<Comment<Thought, int>, int> repository) : base(repository)
        {
        }
    }
}
