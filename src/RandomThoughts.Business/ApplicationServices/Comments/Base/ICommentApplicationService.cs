using RandomThoughts.Business.Base;
using RandomThoughts.Domain;
using RandomThoughts.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomThoughts.Business.ApplicationServices.Comments.Base
{
    public interface ICommentApplicationService<TEntity, TKey> : IBaseApplicationService<Comment<TEntity, TKey>, int> where TEntity:IEntity<TKey>
    {
    }
}
