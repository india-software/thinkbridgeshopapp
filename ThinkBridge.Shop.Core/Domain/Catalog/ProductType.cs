using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkBridge.Shop.Core.Domain.Catalog
{
    public enum ProductType
    {
        /// <summary>
        /// Simple
        /// </summary>
        SimpleProduct = 5,

        /// <summary>
        /// Grouped (product with variants)
        /// </summary>
        GroupedProduct = 10,
    }
}
