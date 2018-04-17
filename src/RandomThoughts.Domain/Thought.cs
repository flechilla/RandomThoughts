using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RandomThoughts.Domain.Base;
using RandomThoughts.Domain.Enums;

namespace RandomThoughts.Domain
{
    public class Thought : AuditableAndTrackableEntity<int>
    {
        /// <summary>
        ///     The title of the Thought
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        ///     The body for the Thought
        /// </summary>
        [Required]
        public string Body { get; set; }

        public ThinkerMood Mood { get; set; }

        /// <summary>
        ///     FK to relate the Thought with the creator.
        /// </summary>
        public string ApplicationUserId { get; set; }

        /// <summary>
        ///     Navigation Prop. to the creator of the Thought.
        /// </summary>
        public ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        ///     Gets or sets the container of the Thought.
        /// </summary>
        public ThoughtHole ThoughtHole { get; set; }

        /// <summary>
        ///     Gets or sets the FK to the related <see cref="ThoughtHole"/>
        /// </summary>
        public int ThoughtHoleId { get; set; }

        /// <summary>
        ///     Gets or sets the amount of likes for the current Thought.
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        ///     Gets or sets the amount of views for the current Thought.
        /// </summary>
        public int Views { get; set; }


    }
}
