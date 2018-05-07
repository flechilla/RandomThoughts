using System;
using System.Collections.Generic;
using System.Text;
using RandomThoughts.Domain.Base;
using RandomThoughts.Domain.Enums;

namespace RandomThoughts.Domain
{
    /// <summary>
    ///    The container of the <see cref="Thought"/>
    /// </summary>
    /// <remarks>
    ///     A <see cref="Thought"/> is related to one <see cref="ThoughtHole"/>
    ///     and a <see cref="ThoughtHole"/> is related to multiple <see cref="Thought"/>
    /// </remarks>
    public class ThoughtHole : AuditableAndTrackableEntity<int>
    {
        /// <summary>
        ///     Gets or sets the Name of the Hole.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the description for the Hole.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     For the relation with the <see cref="Thought"/>
        /// </summary>
        public ICollection<Thought> Thoughts { get; set; }

        /// <summary>
        ///     Gets or sets the Followers of the current <see cref="ThoughtHole"/>
        /// </summary>TODO: Implement in the near future.
       // public ICollection<ApplicationUser> Followers { get; set; }

        /// <summary>
        ///     Gets or sets the amount of Likes of the current HOle.
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        ///     Gets or sets the amount of views of the current Hole.
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// Gets or sets the visibility for the ThoughtHole
        /// </summary>
        public Visibility Visibility { get; set; }
    }
}
