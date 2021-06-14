using System.Collections.Generic;
using System.Threading.Tasks;
using ThinkBridge.Shop.Core;
using ThinkBridge.Shop.Core.Domain.Catalog;

namespace ThinkBridge.Shop.Services
{
    public interface IProductService
    {
        Task DeleteProduct(Product product);
        Task DeleteProductPicture(ProductPicture productPicture);
        Task DeleteProducts(IList<Product> products);
        Task<IList<Product>> GetAllProducts();
        Task<Product> GetProductById(int productId);
        Task<IList<Product>> GetProductByName(string name);
        Task<Product> GetProductBySku(string sku);
        Task<ProductPicture> GetProductPictureById(int productPictureId);
        Task<IList<ProductPicture>> GetProductPicturesByProductId(int productId);
        Task<IList<Product>> GetProductsByIds(int[] productIds);
        Task<IList<Product>> GetProductsBySku(string[] skuArray);
        IDictionary<int, int[]> GetProductsImagesIds(int[] productsIds);
        Task<IList<Product>> GetProductsInCategory(IList<int> categoryIds = null);
        Task InsertProduct(Product product);
        Task InsertProductPicture(ProductPicture productPicture);
        Task UpdateProduct(Product product);
        Task UpdateProductPicture(ProductPicture productPicture);
        Task UpdateProducts(IList<Product> products);
        IThinkBridgePageList<Product> SearchProducts(
           int pageIndex = 0,
           int pageSize = int.MaxValue,
           IList<int> categoryIds = null,
           int manufacturerId = 0,
           string keywords = null
          );
    }
}