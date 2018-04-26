using RandomThoughts.Domain.Base;
using RandomThoughts.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RandomThoughts.Domain
{
    public class Comments : AuditableAndTrackableEntity<int>
    {
        /// <summary>
        ///     Gets or sets FK to relate the Thought with the creator.
        /// </summary>
        public string ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or sets the body for the Comment
        /// </summary>
        [Required]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the FK to the releated <see cref="ParentId"/>
        /// </summary>
        public int ParentId { get; set; }


        /// <summary>
        ///     Gets or sets the amount of likes for the current Thought.
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// Gets or sets the discriminator for the parent of the Commnets
        /// </summary>
        public Discriminator ParentDiscriminator { get; set; }
    }
}
