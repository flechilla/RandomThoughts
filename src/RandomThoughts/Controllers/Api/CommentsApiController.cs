﻿using System;
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

        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<CommentsIndexViewModel> Get(int id, int discriminator)
        {
            var comments = this._commentsApplicationService.ReadAll((id, discriminator)).ToList();
            var commentsView = _mapper.Map<IEnumerable<Comments>, IEnumerable<CommentsIndexViewModel>>(comments);

            return commentsView;
        }

        [HttpPost]
        public IActionResult Post([FromBody]CommentsCreateViewModel newComment)
        {
            if(ModelState.IsValid)
            {
                var comment = _mapper.Map<CommentsCreateViewModel, Comments>(newComment);

                comment.Likes = 0;
                comment.ParentDiscriminator = (RandomThoughts.Domain.Enums.Discriminator)newComment.discriminator;
                comment.ApplicationUserId = this.CurrentUserNickName;
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

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CommentsEditViewModel commentsEdit)
        {
            if (id != commentsEdit.Id)
            {
                ModelState.AddModelError("Id", "The given Id of the edited model doesn't match with the route Id");
                return BadRequest(ModelState);
            }
            if (!_commentsApplicationService.Exists(id))
                return NotFound(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var originalComment = _commentsApplicationService.SingleOrDefault(id);
            originalComment.Body = commentsEdit.Body;

            originalComment.ModifiedBy = this.CurrentUserId;

            _commentsApplicationService.Update(originalComment);


            try
            {
                _commentsApplicationService.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

            return Ok(originalComment);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_commentsApplicationService.Exists(id))
                return NotFound(id);

            _commentsApplicationService.Remove(id);

            try
            {
                _commentsApplicationService.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

            return StatusCode(204);
        }
    }
}