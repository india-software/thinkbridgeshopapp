using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThinkBridge.Shop.Api.ViewModel.Catalog
{
    public class ManufacturerModel : BaseEntityModel
    {
        [Required(ErrorMessage = "Name can't be blank")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public int PictureId { get; set; }
        public string PictureUrl { get; set; }
        public int PageSize { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public int DisplayOrder { get; set; }
    }
}
