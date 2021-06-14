using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThinkBridge.Shop.Api.ViewModel.Catalog
{
    public class ProductManufacturerModel : BaseEntityModel
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Product Id Can't be null")]
        [Range(1, int.MaxValue, ErrorMessage = "Product Id  can't be 0")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Manufacturer Id Can't be null")]
        [Range(1, int.MaxValue, ErrorMessage = "Manufacturer id can't be 0")]
        public int ManufacturerId { get; set; }
        public bool IsFeaturedProduct { get; set; }
        public int DisplayOrder { get; set; }
    }
}
