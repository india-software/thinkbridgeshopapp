using System.Collections.Generic;
using System.Threading.Tasks;
using ThinkBridge.Shop.Core.Domain.Catalog;

namespace ThinkBridge.Shop.Services
{
    /// <summary>
    /// Category service interface
    /// </summary>
    public partial interface ICategoryService
    {
        
        IList<Category> GetAllCategories();
        Task<Category> GetCategoryByName(string categoryName);
        Task<Category> GetCategoryById(int categoryId);
        Task InsertCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(Category category);
        Task DeleteProductCategory(ProductCategory productCategory);
        IList<ProductCategory> GetProductCategoriesByProductId(int productId);
        Task<ProductCategory> GetProductCategoryById(int productCategoryId);
        Task InsertProductCategory(ProductCategory productCategory);
        Task UpdateProductCategory(ProductCategory productCategory);
        List<Category> GetCategoriesByIds(int[] categoryIds);
        IList<int> GetChildCategoryIds(int categoryId);

    }
}