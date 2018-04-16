using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RandomThoughts.Models.ThoughtViewModels
{
    public class ThoughtBaseViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
