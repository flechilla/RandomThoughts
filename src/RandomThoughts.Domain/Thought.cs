using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string UserId => CreatedBy;
    }
}
