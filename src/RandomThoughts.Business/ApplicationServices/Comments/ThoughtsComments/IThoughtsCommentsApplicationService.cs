using RandomThoughts.Business.ApplicationServices.Comments.Base;
using RandomThoughts.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomThoughts.Business.ApplicationServices.Comments.ThoughtsComments
{
    public interface IThoughtsCommentsApplicationService : ICommentApplicationService<Thought, int>
    {
    }
}
