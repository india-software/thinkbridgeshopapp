using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkBridge.Shop.Core.Domain
{
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Get or sets the date and time of instance update
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
}
