﻿using RandomThoughts.Business.ApplicationServices.Comments;
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
    public class ThoughtCommentApplicationService : BaseApplicationService<RandomThoughts.Domain.Comment,int>, IThoughtCommentApplicationService
    {
        public ThoughtCommentApplicationService(ICommentsRepository repository) : base(repository)
        {
        }

        public void AddComment(Comment comments)
        {
            comments.ParentDiscriminator = Discriminator.Thought;
            this.Add(comments);
        }

        public IQueryable<Domain.Comment> ReadAll(int idparent)
        {
            var repository = (Repository as ICommentsRepository);
            return repository.ReadAll((idparent, Discriminator.Thought));
        }

        public IQueryable<Comment> ReadAll(int idparent, int Count)
        {
            var repository = (Repository as ICommentsRepository);
            return repository.ReadAll((idparent, Discriminator.Thought),Count);
        }

        public async Task<IQueryable<Comment>> ReadAllAsync(int idparent)
        {
            var repository = (Repository as ICommentsRepository);
            return await repository.ReadAllAsync((idparent, Discriminator.Thought));
        }

        public async Task<IQueryable<Comment>> ReadAllAsync(int idparent, int Count)
        {
            var repository = (Repository as ICommentsRepository);
            return await repository.ReadAllAsync((idparent, Discriminator.Thought),Count);
        }
    }
}
