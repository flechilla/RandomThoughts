using Microsoft.AspNetCore.Mvc;
using RandomThoughts.Models.CommentViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomThoughts.Controllers.Api
{
    public interface ICommentController: IBaseApiController
    {
        [HttpGet]
        IEnumerable<CommentsIndexViewModel> GetAllComments(int ParentId);

        [HttpGet("{id}")]
        IActionResult GetComments(int id);

        [HttpPost]
        IActionResult PostComment([FromBody]CommentsCreateViewModel newComment);

        [HttpPut("{id}")]
        IActionResult PutComment(int id, [FromBody]CommentsEditViewModel commentsEdit);

        [HttpDelete("{id}")]
        IActionResult DeleteComment(int id);
    }
}
