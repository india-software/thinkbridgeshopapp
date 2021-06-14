using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThinkBridge.Shop.Core.Caching;
using ThinkBridge.Shop.Core.Domain.Catalog;
using ThinkBridge.Shop.Data;

namespace ThinkBridge.Shop.Services
{
    /// <summary>
    /// Category service
    /// </summary>
    public partial class CategoryService : ICategoryService
    {
        #region Fields
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly string _entityName;

        #endregion

        #region Ctor

        public CategoryService(
            IRepository<Category> categoryRepository,
            IRepository<Product> productRepository,
            IRepository<ProductCategory> productCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _entityName = typeof(Category).Name;
        }

        #endregion

        #region Methods

        public async Task DeleteCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));
            category.Deleted = true;
           await UpdateCategory(category);
        }
        public virtual IList<Category> GetAllCategories()
        {

            var query = from p in _categoryRepository.Table
                        where !p.Deleted
                        select p;
            return query.ToList();
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            Category category = null;
            category = await Task.Run(() =>
            {
                var query = _categoryRepository.Table;

                if (!string.IsNullOrEmpty(categoryName) && query != null && query.Count() > 0)
                    category = query.FirstOrDefault(c => c.Name.Contains(categoryName));
                return category;
            });
            return category;


        }
        public async Task<Category> GetCategoryById(int categoryId)
        {
            if (categoryId == 0)
                return null;

            return await _categoryRepository.GetById(categoryId);
        }

        /// <summary>
        /// Inserts category
        /// </summary>
        /// <param name="category">Category</param>
        public async Task InsertCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

           await _categoryRepository.Insert(category);
        }

        public async Task UpdateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));
            await _categoryRepository.Update(category);
        }

        public async Task DeleteProductCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException(nameof(productCategory));
            await _productCategoryRepository.Delete(productCategory);
        }
        public virtual IList<ProductCategory> GetProductCategoriesByProductId(int productId)
        {
            if (productId == 0)
                return new List<ProductCategory>();

            var query = from pc in _productCategoryRepository.Table
                        join c in _categoryRepository.Table on pc.CategoryId equals c.Id
                        where pc.ProductId == productId &&
                              !c.Deleted && c.Published
                        orderby pc.DisplayOrder, pc.Id
                        select pc;

            return query.ToList();

        }

        public async Task<ProductCategory> GetProductCategoryById(int productCategoryId)
        {
            if (productCategoryId == 0)
                return null;

            return await _productCategoryRepository.GetById(productCategoryId);
        }

        public async Task InsertProductCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException(nameof(productCategory));

           await _productCategoryRepository.Insert(productCategory);

        }
        public async Task UpdateProductCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException(nameof(productCategory));

            await _productCategoryRepository.Update(productCategory);
        }

        public List<Category> GetCategoriesByIds(int[] categoryIds)
        {
            if (categoryIds == null || categoryIds.Length == 0)
                return new List<Category>();

            var query = from p in _categoryRepository.Table
                        where categoryIds.Contains(p.Id) && !p.Deleted
                        select p;

            return query.ToList();
        }
        /// <summary>
        /// Get All Child Category Ids
        /// </summary>
        /// <param name="parentCategoryId"></param>
        /// <returns></returns>
        public virtual IList<int> GetChildCategoryIds(int parentCategoryId)
        {
            //little hack for performance optimization, We need to optimize performanace in this code.
                var categoriesIds = new List<int>();
            var categories = GetAllCategories()
                .Where(c => c.ParentCategoryId == parentCategoryId);
            foreach (var category in categories)
            {
                categoriesIds.Add(category.Id);
                categoriesIds.AddRange(GetChildCategoryIds(category.Id));
            }
            return categoriesIds;
        }

        
        #endregion
    }
}