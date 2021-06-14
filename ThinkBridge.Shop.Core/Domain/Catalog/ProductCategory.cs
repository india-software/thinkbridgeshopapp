using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkBridge.Shop.Core.Domain.Catalog
{
    public partial class ProductCategory : BaseEntity
    {
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the category identifier
        /// </summary>
        public int CategoryId { get; set; }       

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// Gets the category
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Gets the product
        /// </summary>
        public virtual Product Product { get; set; }

    }
}
