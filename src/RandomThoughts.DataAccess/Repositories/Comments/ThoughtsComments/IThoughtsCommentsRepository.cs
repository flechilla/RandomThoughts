using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;

namespace RandomThoughts.DataAccess.Repositories.Comments.ThoughtsComments
{
    public interface IThoughtsCommentsRepository : ICommentsRepository<Thought, int>
    {

    }
}
