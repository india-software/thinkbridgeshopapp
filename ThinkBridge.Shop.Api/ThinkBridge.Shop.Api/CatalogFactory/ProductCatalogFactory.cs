using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThinkBridge.Shop.Api.Mapper;
using ThinkBridge.Shop.Api.ViewModel;
using ThinkBridge.Shop.Api.ViewModel.Catalog;
using ThinkBridge.Shop.Core.Domain.Catalog;
using ThinkBridge.Shop.Services;
using ThinkBridge.Shop.Services.Media;

namespace ThinkBridge.Shop.Api.CatalogFactory
{
    public class ProductCatalogFactory : IProductCatalogFactory
    {
        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductService _productService;
        private readonly IPictureService _pictureService;

        #endregion

        #region Ctor

        public ProductCatalogFactory(
            ICategoryService categoryService,
            IManufacturerService manufacturerService,
            IProductService productService,
            IPictureService pictureService)
        {
            _categoryService = categoryService;
            _manufacturerService = manufacturerService;
            _productService = productService;
            _pictureService = pictureService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare product search model
        /// </summary>
        /// <param name="searchModel">Product search model</param>
        /// <returns>Product search model</returns>
        public  ProductSearchModel PrepareProductSearchModel(ProductSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare available categories
            var categories = _categoryService.GetAllCategories();
            if (categories != null && categories.Count > 0)
            {
                searchModel.AvailableCategories = categories.Select(x =>
                           new GenericDropDownItem()
                           {
                               Id = x.Id,
                               Name = x.Name,
                               PicUrl = x.PictureId > 0 ? _pictureService.GetPictureUrl(x.PictureId) : ""
                           }).ToList();
            }
            var manufacturers = _manufacturerService.GetAllManufacturer();
            if (manufacturers != null && manufacturers.Count > 0)
            {
                searchModel.AvailableManufacturers = manufacturers.Select(x =>
                           new GenericDropDownItem()
                           {
                               Id = x.Id,
                               Name = x.Name,
                               PicUrl = x.PictureId > 0 ? _pictureService.GetPictureUrl(x.PictureId) : ""
                           }).ToList();
            }


            return searchModel;
        }

        /// <summary>
        /// Prepare paged product list model
        /// </summary>
        /// <param name="searchModel">Product search model</param>
        /// <returns>Product list model</returns>
        public virtual ProductListModel PrepareProductListModel(ProductSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));


            var categoryIds = new List<int> { searchModel.SearchCategoryId };
            if (searchModel.SearchCategoryId > 0)
            {
                var childCategoryIds = _categoryService.GetChildCategoryIds(searchModel.SearchCategoryId);
                categoryIds.AddRange(childCategoryIds);
            }

            //get products
            var products =  _productService.SearchProducts(
                categoryIds: categoryIds,
                manufacturerId: searchModel.SearchManufacturerId,
                keywords: searchModel.SearchProductName,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new ProductListModel
            {
                Data = products.Select(product =>
                {
                    return PrepareProductModel(null, product);
                }),
                Total = products.TotalCount
            };

            return model;
        }

        /// <summary>
        /// Prepare product model
        /// </summary>
        /// <param name="model">Product model</param>
        /// <param name="product">Product</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Product model</returns>
        public virtual ProductModel PrepareProductModel(ProductModel model, Product product)
        {
            if (product != null)
            {
                //fill in model values from the entity
                if (model == null)
                {
                    model = product.ToModel<ProductModel>();
                    model.ProductPictures = PrepareProductPictureListModel(product.Id);
                    model.ProductManufacturers = PrepareProductManufacturerListModel(product.Id);
                    model.ProductCategories = PrepareProductCategoryListModel(product.Id);
                }
            }
            return model;
        }
        /// <summary>
        /// Prepare paged product picture list model
        /// </summary>       
        /// <param name="productId">ProductId</param>
        /// <returns>Product picture list model</returns>
        public virtual List<ProductPictureModel> PrepareProductPictureListModel(int productId)
        {
            var prodPicModels = new List<ProductPictureModel>();
            var productPictures = _productService.GetProductPicturesByProductId(productId).Result;
            if (productPictures != null && productPictures.Count > 0)
            {
                prodPicModels = productPictures.Select(x => new ProductPictureModel()
                {
                    Id = x.Id,
                    PictureId = x.PictureId,
                    PictureUrl = _pictureService.GetPictureUrl(x.PictureId),
                    DisplayOrder = x.DisplayOrder,
                    ProductId = x.ProductId
                }).ToList();
            }
            return prodPicModels;
        }
        /// <summary>
        /// Product Category List Model
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public virtual List<ProductCategoryModel> PrepareProductCategoryListModel(int productId)
        {
            var productCategories = new List<ProductCategoryModel>();
            var categories = _categoryService.GetProductCategoriesByProductId(productId);
            if (categories != null && categories.Count > 0)
            {
                productCategories = categories.Select(x => new ProductCategoryModel()
                {
                    Id = x.Id,
                    CategoryId = x.CategoryId,
                    Name = _categoryService.GetCategoryById(x.CategoryId).Result.Name,
                    ProductId = x.ProductId
                }).ToList();
            }
            return productCategories;
        }
        /// <summary>
        /// Product Manufacturer List Model
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public virtual List<ProductManufacturerModel> PrepareProductManufacturerListModel(int productId)
        {
            var prodManufacturers = new List<ProductManufacturerModel>();
            var manufacturers = _manufacturerService.GetProductManufacturersByProductId(productId);
            if (manufacturers != null && manufacturers.Count > 0)
            {
                prodManufacturers = manufacturers.Select(x => new ProductManufacturerModel()
                {
                    Id = x.Id,
                    ManufacturerId = x.ManufacturerId,
                    Name = _manufacturerService.GetManufacturerById(x.ManufacturerId).Result.Name,
                    ProductId = x.ProductId,
                    DisplayOrder = x.DisplayOrder,
                    IsFeaturedProduct = x.IsFeaturedProduct
                }).ToList();
            }
            return prodManufacturers;
        }

        #endregion
    }
}
