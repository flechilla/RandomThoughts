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
    public class CommentsApiController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ICommentsApplicationService _commentsApplicationService;
        public CommentsApiController(IHttpContextAccessor httpContextAccessor,
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

        [HttpPost]
        public IActionResult Post([FromBody]CommentsCreateViewModel newComment)
        {
            if(ModelState.IsValid)
            {
                var comment = _mapper.Map<CommentsCreateViewModel, Comments>(newComment);

                comment.Likes = 0;
                comment.ParentDiscriminator = (RandomThoughts.Domain.Enums.Discriminator)newComment.discriminator;
                comment.ApplicationUserId = this.CurrentUserId;
                this._commentsApplicationService.Add(comment);

                try
                {
                    _commentsApplicationService.SaveChanges();
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

                return Created("", comment);
            }
            
            return BadRequest(ModelState);            
        }
    }
}
