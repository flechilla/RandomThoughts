using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomThoughts.Models.CommentViewModel
{
    public class CommentsIndexViewModel : CommentsBaseViewModel
    {
        public int Id { get; set; }

        public string CreateAtHumanized { get; set; }

        public string ModifiedAtHumanized { get; set; }
    }
}
