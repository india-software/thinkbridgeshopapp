using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThinkBridge.Shop.Core;
using ThinkBridge.Shop.Core.Caching;
using ThinkBridge.Shop.Core.Domain.Catalog;
using ThinkBridge.Shop.Core.Helper;
using ThinkBridge.Shop.Data;
using ThinkBridge.Shop.Services.Media;

namespace ThinkBridge.Shop.Services
{
    /// <summary>
    /// Product service
    /// </summary>
    public partial class ProductService : IProductService
    {
        #region Fields

        private readonly IDbContext _dbContext;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductPicture> _productPictureRepository;
        private readonly IPictureService _pictureService;
        public string _entityName;
        #endregion

        #region Ctor

        public ProductService(IDbContext dbContext,
            ICacheManager cacheManager,
            IRepository<Product> productRepository,
            IRepository<ProductPicture> productPictureRepository, IPictureService pictureService)
        {
            _dbContext = dbContext;
            _cacheManager = cacheManager;
            _pictureService = pictureService;
            _productRepository = productRepository;
            _productPictureRepository = productPictureRepository;
            _entityName = typeof(Product).Name;

        }

        #endregion


        #region Methods
        #region Products
        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="product">Product</param>
        public async Task DeleteProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            product.Deleted = true;
            //delete product
          await  UpdateProduct(product);
        }

        /// <summary>
        /// Delete products
        /// </summary>
        /// <param name="products">Products</param>
        public async Task DeleteProducts(IList<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            foreach (var product in products)
            {
                product.Deleted = true;
            }
            //to delete product, we need to save deleted as true
           await UpdateProducts(products);

        }


        /// <summary>
        /// Gets product
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product</returns>
        public async Task<Product> GetProductById(int productId)
        {
            if (productId == 0)
                return null;

            var key = string.Format("product_", productId);
            return await _cacheManager.Get(key, () => _productRepository.GetById(productId));
        }

        /// <summary>
        /// Get products by identifiers
        /// </summary>
        /// <param name="productIds">Product identifiers</param>
        /// <returns>Products</returns>
        public async Task<IList<Product>> GetProductsByIds(int[] productIds)
        {
            var sortedProducts = new List<Product>();
            sortedProducts = await Task.Run(() =>
             {
                 if (productIds == null || productIds.Length == 0)
                     return new List<Product>();

                 var query = from p in _productRepository.Table
                             where productIds.Contains(p.Id) && !p.Deleted
                             select p;
                 var products = query.ToList();
                //sort by passed identifiers           
                foreach (var id in productIds)
                 {
                     var product = products.Find(x => x.Id == id);
                     if (product != null)
                         sortedProducts.Add(product);
                 }
                 return sortedProducts;
             });
            return sortedProducts;
        }
        public async Task<IList<Product>> GetAllProducts()
        {
            var query = await Task.Run(() =>
            {
                return from p in _productRepository.Table
                       where !p.Deleted
                       select p;
            });
            return query.ToList();
        }

        /// <summary>
        /// Inserts a product
        /// </summary>
        /// <param name="product">Product</param>
        public async Task InsertProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            //insert
            await _productRepository.Insert(product);

            //clear cache
            _cacheManager.RemoveByPattern("product_");

        }

        /// <summary>
        /// Updates the product
        /// </summary>
        /// <param name="product">Product</param>
        public async Task UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            //update
         await   _productRepository.Update(product);

            //cache
            _cacheManager.RemoveByPattern("product_");
        }

        /// <summary>
        /// Update products
        /// </summary>
        /// <param name="products">Products</param>
        public async Task UpdateProducts(IList<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            //update
           await _productRepository.Update(products);

            //cache
            _cacheManager.RemoveByPattern("product_");

        }


        /// <summary>
        /// Get number of product (published and visible) in certain category
        /// </summary>
        /// <param name="categoryIds">Category identifiers</param>
        /// <returns>Number of products</returns>
        public async Task<IList<Product>> GetProductsInCategory(IList<int> categoryIds = null)
        {                     
           return await Task.Run(() =>
            {
                var productList = new List<Product>();
                //validate "categoryIds" parameter  
                if (categoryIds != null && categoryIds.Contains(0))
                    categoryIds.Remove(0);

                //category filtering
                if (categoryIds != null && categoryIds.Any())
                {
                    var query = _productRepository.Table.Where(p => !p.Deleted && p.Published);
                    query = from p in query
                            from pc in p.ProductCategories.Where(pc => categoryIds.Contains(pc.CategoryId))
                            select p;
                    productList = query.Distinct().ToList();
                }
                return productList;
            });
            
        }

        /// <summary>
        /// Gets a product by name
        /// </summary>
        /// <param name="name">SKU</param>
        /// <returns>Product</returns>
        public async Task<IList<Product>> GetProductByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            name = name.Trim();
            var query = await Task.Run(() =>
            {
                  return from p in _productRepository.Table
                         orderby p.Id
                         where
                       p.Name == name
                         select p;
            });
            return query.ToList();
        }


        /// <summary>
        /// Gets a product by SKU
        /// </summary>
        /// <param name="sku">SKU</param>
        /// <returns>Product</returns>
        public async Task<Product> GetProductBySku(string sku)
        {
            if (string.IsNullOrEmpty(sku))
                return null;

            sku = sku.Trim();

            var query = await Task.Run(() =>
            {
               return from p in _productRepository.Table
                orderby p.Id
                where !p.Deleted &&
                p.Sku == sku
                select p;
            });
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Gets a products by SKU array
        /// </summary>
        /// <param name="skuArray">SKU array</param>
        /// <param name="vendorId">Vendor ID; 0 to load all records</param>
        /// <returns>Products</returns>
        public async Task<IList<Product>> GetProductsBySku(string[] skuArray)
        {
            if (skuArray == null)
                throw new ArgumentNullException(nameof(skuArray));

            var query = await Task.Run(() =>
             {
                 return _productRepository.Table.Where(p => !p.Deleted && skuArray.Contains(p.Sku));
             });
            return query.ToList();
        }

        #endregion

        #region Product pictures

        /// <summary>
        /// Deletes a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        public async Task DeleteProductPicture(ProductPicture productPicture)
        {
            if (productPicture == null)
                throw new ArgumentNullException(nameof(productPicture));

           await _productPictureRepository.Delete(productPicture);

        }

        /// <summary>
        /// Gets a product pictures by product identifier
        /// </summary>
        /// <param name="productId">The product identifier</param>
        /// <returns>Product pictures</returns>
        public async Task<IList<ProductPicture>> GetProductPicturesByProductId(int productId)
        {
            var query = await Task.Run(() =>
            {
                return from pp in _productPictureRepository.Table
                       where pp.ProductId == productId
                       orderby pp.DisplayOrder, pp.Id
                       select pp;
            });
            return query.ToList();
        }

        /// <summary>
        /// Gets a product picture
        /// </summary>
        /// <param name="productPictureId">Product picture identifier</param>
        /// <returns>Product picture</returns>
        public async Task<ProductPicture> GetProductPictureById(int productPictureId)
        {
            if (productPictureId == 0)
                return null;

            return await _productPictureRepository.GetById(productPictureId);
        }

        /// <summary>
        /// Inserts a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        public async Task InsertProductPicture(ProductPicture productPicture)
        {
            if (productPicture == null)
                throw new ArgumentNullException(nameof(productPicture));

          await  _productPictureRepository.Insert(productPicture);

        }

        /// <summary>
        /// Updates a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        public async Task UpdateProductPicture(ProductPicture productPicture)
        {
            if (productPicture == null)
                throw new ArgumentNullException(nameof(productPicture));

          await  _productPictureRepository.Update(productPicture);

        }

        /// <summary>
        /// Get the IDs of all product images 
        /// </summary>
        /// <param name="productsIds">Products IDs</param>
        /// <returns>All picture identifiers grouped by product ID</returns>
        public IDictionary<int, int[]> GetProductsImagesIds(int[] productsIds)
        {
            var productPictures = _productPictureRepository.Table.Where(p => productsIds.Contains(p.ProductId)).ToList();

            return productPictures.GroupBy(p => p.ProductId).ToDictionary(p => p.Key, p => p.Select(p1 => p1.PictureId).ToArray());
        }

        #endregion
        #region product search and filter
        public IThinkBridgePageList<Product> SearchProducts(         
           int pageIndex = 0,
           int pageSize = int.MaxValue,
           IList<int> categoryIds = null,
           int manufacturerId = 0,
           string keywords = null
          )
        {
            //validate "categoryIds" parameter
            if (categoryIds != null && categoryIds.Contains(0))
                categoryIds.Remove(0);

            //pass category identifiers as comma-delimited string
            var commaSeparatedCategoryIds = categoryIds == null ? string.Empty : string.Join(",", categoryIds);

            //some databases don't support int.MaxValue
            if (pageSize == int.MaxValue)
                pageSize = int.MaxValue - 1;

            //prepare input parameters
            var pCategoryIds = DataProviderHelper.GetStringParameter("CategoryIds", commaSeparatedCategoryIds);
            var pManufacturerId = DataProviderHelper.GetInt32Parameter("ManufacturerId", manufacturerId);            
            var pKeywords = DataProviderHelper.GetStringParameter("Keywords", keywords);           
             var pPageIndex = DataProviderHelper.GetInt32Parameter("PageIndex", pageIndex);
            var pPageSize = DataProviderHelper.GetInt32Parameter("PageSize", pageSize);           
            var pTotalRecords = DataProviderHelper.GetOutputInt32Parameter("TotalRecords");
            //invoke stored procedure

            var query = _dbContext.EntityFromSql<Product>("ThinkBridgeProductSearch",
            pPageIndex, pPageSize, pCategoryIds, pManufacturerId, pKeywords, pTotalRecords);
            var products = query.ToList();
           

            //return products
            var totalRecords = pTotalRecords.Value != DBNull.Value ? Convert.ToInt32(pTotalRecords.Value) : 0;
            return new ThinkBridgePageList<Product>(products, pageIndex, pageSize, totalRecords);
        }

       


        #endregion

        #endregion
    }
}