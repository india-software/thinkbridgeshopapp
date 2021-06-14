using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThinkBridge.Shop.Api.ViewModel
{
    public class ProductCategoryModel : BaseEntityModel
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Product Id Can't be null")]
        [Range(1, int.MaxValue, ErrorMessage = "Product id can't be 0")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Category Id Can't be null")]
        [Range(1, int.MaxValue, ErrorMessage = "Category id can't be 0")]
        public int CategoryId { get; set; }
    }
}
