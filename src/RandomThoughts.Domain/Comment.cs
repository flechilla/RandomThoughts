using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using RandomThoughts.Domain.Base;

using RandomThoughts.Domain.Enums;


namespace RandomThoughts.Domain
{
    public class Comment<TEntity,TKey> : AuditableAndTrackableEntity<int> where TEntity:IEntity<TKey>
    {
        /// <summary>
        /// Gets or sets the body for the Comment
        /// </summary>
        [Required]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the FK to the releated <see cref="Thought"/>
        /// </summary>
        public TKey EntityId { get; set; }

        /// <summary>
        /// Get or sets the Thought for the Comment
        /// </summary>
        public TEntity Entity { get; set; }

        /// <summary>
        ///     Gets or sets FK to relate the Thought with the creator.
        /// </summary>
        public string ApplicationUserId { get; set; }


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
