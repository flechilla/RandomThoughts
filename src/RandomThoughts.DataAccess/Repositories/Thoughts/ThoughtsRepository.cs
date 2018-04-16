using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.DataAccess.Repositories.Base;
using RandomThoughts.Domain;

namespace RandomThoughts.DataAccess.Repositories.Thoughts
{
    public class ThoughtsRepository : BaseRepository<Thought, int>, IThoughtsRepository
    {
        public ThoughtsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
