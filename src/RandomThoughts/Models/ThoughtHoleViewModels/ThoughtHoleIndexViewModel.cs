using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomThoughts.Models.ThoughtHoleViewModels
{
    public class ThoughtHoleIndexViewModel : ThoughtHoleBaseViewModel
    {
        public int Id { get; set; }

        public string CreateAtHumanized { get; set; }

        public string ModifiedAtHumanized { get; set; }

        public int Likes { get; set; }

        public int Views { get; set; }

        /// <summary>
        ///     Gets or sets the value that indicates
        ///     the amount of Thoughts that this Hole
        ///     contains.
        /// </summary>
        public int AmountOfThought { get; set; }
    }
}
