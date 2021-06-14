using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkBridge.Shop.Core.Domain.Media
{
    public partial class Picture : BaseEntity
    {
        /// <summary>
        /// Gets or sets the picture mime type
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the SEO friendly filename of the picture
        /// </summary>
        public string SeoFilename { get; set; }

        /// <summary>
        /// Gets or sets the "alt" attribute for "img" HTML element. If empty, then a default rule will be used (e.g. product name)
        /// </summary>
        public string AltAttribute { get; set; }

        /// <summary>
        /// Gets or sets the "title" attribute for "img" HTML element. If empty, then a default rule will be used (e.g. product name)
        /// </summary>
        public string TitleAttribute { get; set; }
        /// <summary>
        /// Get or sets the extension of image to get picture url
        /// </summary>
        public string Extension { get; set; }
       
        
    }
}
