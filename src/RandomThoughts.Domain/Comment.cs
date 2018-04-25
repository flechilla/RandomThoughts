using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using RandomThoughts.Domain.Base;
using RandomThoughts.Domain.Enums;


namespace RandomThoughts.Domain
{
    public class Comment : AuditableAndTrackableEntity<int>
    {
        /// <summary>
        /// Gets or sets the body for the Comment
        /// </summary>
        [Required]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the FK to the releated <see cref="Thought"/>
        /// </summary>
        public int ThoughtId { get; set; }

        /// <summary>
        /// Get or sets the Thought for the Comment
        /// </summary>
        public Thought Thought { get; set; }

        /// <summary>
        ///     Gets or sets FK to relate the Thought with the creator.
        /// </summary>
        public string ApplicationUserId { get; set; }

        /// <summary>
        ///     Gets or sets the Navigation Prop. to the creator of the Thought.
        /// </summary>
        public ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        ///     Gets or sets the amount of likes for the current Thought.
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// Gets or sets the visibility level for the Comment
        /// </summary>
        public Visibility Visibility { get; set; }
    }
}
