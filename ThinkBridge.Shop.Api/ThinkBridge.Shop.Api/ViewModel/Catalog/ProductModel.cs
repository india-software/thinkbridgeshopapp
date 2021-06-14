using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ThinkBridge.Shop.Api.ViewModel.Catalog;
using ThinkBridge.Shop.Core.Domain.Catalog;

namespace ThinkBridge.Shop.Api.ViewModel
{

   public partial class ProductModel: BaseEntityModel
    {

        [Required(ErrorMessage ="Product Name Can't be blank")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ShortDescription  Can't be blank")]
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public bool ShowOnHomePage { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public bool AllowCustomerReviews { get; set; }
        [Required(ErrorMessage = "Sku  Can't be blank")]
        public string Sku { get; set; }
        public bool IsShipEnabled { get; set; }
        public bool IsFreeShipping { get; set; }
        public bool ShipSeparately { get; set; }
        public decimal AdditionalShippingCharge { get; set; }
        public int StockQuantity { get; set; }
        public bool DisplayStockAvailability { get; set; }
        public bool DisplayStockQuantity { get; set; }
        public int MinStockQuantity { get; set; }
        public int NotifyAdminForQuantityBelow { get; set; }
        public int OrderMinimumQuantity { get; set; }
        public int OrderMaximumQuantity { get; set; }
        public bool NotReturnable { get; set; }
        public bool DisableBuyButton { get; set; }
        public bool AvailableForPreOrder { get; set; }
        public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; set; }
        public bool CallForPrice { get; set; }
        [Required(ErrorMessage = "Sku  Can't be blank")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public decimal ProductCost { get; set; }
        public decimal BasepriceAmount { get; set; }
        public decimal Weight { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public DateTime? AvailableStartDateTimeUtc { get; set; }
        public DateTime? AvailableEndDateTimeUtc { get; set; }
        public int DisplayOrder { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        
        public virtual IList<ProductCategoryModel> ProductCategories { get; set; }
        
        public virtual IList<ProductManufacturerModel> ProductManufacturers { get; set; }
        
        public virtual IList<ProductPictureModel> ProductPictures { get; set; }
    }
}

