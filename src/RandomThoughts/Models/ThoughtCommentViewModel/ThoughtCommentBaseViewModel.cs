using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RandomThoughts.Domain.Enums;

namespace RandomThoughts.Models.ThoughtCommentViewModel
{
    public class ThoughtCommentBaseViewModel
    {
        [Required]
        [MaxLength(255)]
        public string Body { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        
        public int Likes { get; set; }
    }
}
