using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomThoughts.Business.ApplicationServices.Comments;
using RandomThoughts.DataAccess.Repositories.Comments;
using RandomThoughts.Domain;
using RandomThoughts.Models.ThoughtCommentViewModel;

namespace RandomThoughts.Controllers.Api
{
    [Produces("application/json")]
    public class CommentsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ICommentsApplicationService _commentsApplicationService;
        public CommentsController(IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            ICommentsApplicationService commentsApplicationService) : base(httpContextAccessor)
        {
            this._mapper = mapper;
            this._commentsApplicationService = commentsApplicationService;
        }

        [HttpGet]
        public IEnumerable<Comments> Get(int id,int discrimator)
        {
            return this._commentsApplicationService.ReadAll((id,discrimator)).ToList();
        }
    }
}
