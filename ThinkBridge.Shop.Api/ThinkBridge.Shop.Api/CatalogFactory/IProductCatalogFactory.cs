using System.Collections.Generic;
using ThinkBridge.Shop.Api.ViewModel;
using ThinkBridge.Shop.Api.ViewModel.Catalog;
using ThinkBridge.Shop.Core.Domain.Catalog;

namespace ThinkBridge.Shop.Api.CatalogFactory
{
    public interface IProductCatalogFactory
    {
        List<ProductCategoryModel> PrepareProductCategoryListModel(int productId);
        ProductListModel PrepareProductListModel(ProductSearchModel searchModel);
        List<ProductManufacturerModel> PrepareProductManufacturerListModel(int productId);
        ProductModel PrepareProductModel(ProductModel model, Product product);
        List<ProductPictureModel> PrepareProductPictureListModel(int productId);
        ProductSearchModel PrepareProductSearchModel(ProductSearchModel searchModel);
    }
}