using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThinkBridge.Shop.Api.ViewModel.Catalog
{
    public class ProductPictureModel : BaseEntityModel
    {
        [Required(ErrorMessage = "Product Id Can't be null")]
        [Range(1, int.MaxValue, ErrorMessage = "Product id can't be 0")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Picture Id Can't be null")]
        [Range(1, int.MaxValue, ErrorMessage = "Picture id can't be 0")]
        public int PictureId { get; set; }
        
        public string PictureUrl { get; set; }
       
        public int DisplayOrder { get; set; }

    }
}
