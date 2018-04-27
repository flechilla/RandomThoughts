using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomThoughts.Models.ThoughtCommentViewModel
{
    public class CommentsCreateViewModel : CommentsBaseViewModel
    {
        public int ParentId { get; set; }

        public int discriminator { get; set; }
    }
}
