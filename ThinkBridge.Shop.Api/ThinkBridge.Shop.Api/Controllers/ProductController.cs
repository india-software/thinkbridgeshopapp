using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThinkBridge.Shop.Api.ViewModel;
using ThinkBridge.Shop.Core.Domain.Catalog;
using ThinkBridge.Shop.Services;
using ThinkBridge.Shop.Api.Mapper;
using ThinkBridge.Shop.Api.ViewModel.Catalog;
using ThinkBridge.Shop.Api.CatalogFactory;
using ThinkBridge.Shop.Services.Media;

namespace ThinkBridge.Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductCatalogFactory _productCatalogFactory;
        private readonly IPictureService _pictureService;
        public ProductController(IProductService productService, 
            ICategoryService categoryService,
            IManufacturerService manufacturerService,
            IProductCatalogFactory productCatalogFactory,
            IPictureService pictureService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _manufacturerService = manufacturerService;
            _productCatalogFactory = productCatalogFactory;
            _pictureService = pictureService;
        }
        [HttpPost]
        [Route("GetProducts")]
        public IActionResult GetProducts([FromBody] ProductSearchModel productSearchModel)
        {
            var result =_productCatalogFactory.PrepareProductListModel(productSearchModel);
            return Ok(result);
        }
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            var model = _productCatalogFactory.PrepareProductModel(null, product);
            if (model == null)
                return NotFound();
            return Ok(model);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] ProductModel productModel)
        {
            var product = productModel.ToEntity<Product>();
            await _productService.InsertProduct(product);
            var productItem = _productCatalogFactory.PrepareProductModel(null, product);
            return Ok(productItem);
        }
        #region product category
        [HttpGet]
        [Route("GetProductCategory/{id:int}")]
        public async Task<ActionResult> GetProductCategory(int Id)
        {
            var pictureModelList = new List<ProductCategoryModel>();
            var productCategories = await Task.Run(() => { return _categoryService.GetProductCategoriesByProductId(Id); });
            foreach (var item in productCategories)
            {
                var prodCategoryModel = item.ToModel<ProductCategoryModel>();
                var category = await _categoryService.GetCategoryById(item.CategoryId);
                if (category != null)
                    prodCategoryModel.Name = category.Name;
                pictureModelList.Add(prodCategoryModel);
            }
            return Ok(pictureModelList);
        }
        [HttpPost]
        [Route("AddProductCategory")]
        public async Task<ActionResult> AddProductCategory([FromBody] ProductCategoryModel productModel)
        {
            var productCategory = productModel.ToEntity<ProductCategory>();
            await _categoryService.InsertProductCategory(productCategory);          
            return Ok(productCategory);
        }
        [HttpPost]
        [Route("DeleteProductCategory")]
        public async Task<ActionResult> DeleteProductCategory([FromBody] ProductCategoryModel productModel)
        {
            var category = _categoryService.GetProductCategoriesByProductId(productModel.ProductId).FirstOrDefault(x => x.CategoryId == productModel.CategoryId);
            if (category == null)
                return NotFound();
            await _categoryService.DeleteProductCategory(category);
            return Ok();
        }
        #endregion

        #region product picture
        [HttpGet]
        [Route("GetProductPicture/{id:int}")]
        public async Task<ActionResult> GetProductPicture(int Id)
        {
            var pictureModelList = new List<ProductPictureModel>();
            var manufacturer = await _productService.GetProductPicturesByProductId(Id);
            foreach (var item in manufacturer)
            {
                var pictureModel = item.ToModel<ProductPictureModel>();
                pictureModelList.Add(pictureModel);
            }
            return Ok(pictureModelList);
        }
        [HttpPost]
        [Route("AddProductPicture")]
        public async Task<ActionResult> AddProductPicture([FromBody] ProductPictureModel productModel)
        {
            var productPicture = productModel.ToEntity<ProductPicture>();
            await _productService.InsertProductPicture(productPicture);
            return Ok(productPicture);
        }
        [HttpPost]
        [Route("DeleteProductPicture")]
        public async Task<ActionResult> DeleteProductPicture([FromBody] ProductPictureModel productModel)
        {
            var allPicture = await _productService.GetProductPicturesByProductId(productModel.ProductId);
            var item = allPicture.FirstOrDefault(x => x.PictureId == productModel.PictureId);
            if (item == null)
                return NotFound();
            await _productService.DeleteProductPicture(item);
            return Ok();
        }
        #endregion

        #region product manufacturer
        [HttpGet]
        [Route("GetProductManufacturer/{id:int}")]
        public async Task<ActionResult> GetProductManufacturer(int Id)
        {
            
            var manufacturerModelList = new List<ProductManufacturerModel>();
            manufacturerModelList = await Task.Run(() =>
             {
                 var manufacturer = _manufacturerService.GetProductManufacturersByProductId(Id);
                 foreach (var item in manufacturer)
                 {
                     var manufacturerModel = item.ToModel<ProductManufacturerModel>();
                     var itemMan = _manufacturerService.GetManufacturerById(item.ManufacturerId).Result;
                     if (itemMan != null)
                         manufacturerModel.Name = itemMan.Name;
                     manufacturerModelList.Add(manufacturerModel);
                 }
                 return manufacturerModelList;
             });
            return Ok(manufacturerModelList);
        }
        [HttpPost]
        [Route("AddProductManufacturer")]
        public async Task<ActionResult> AddProductManufacturer([FromBody] ProductManufacturerModel productModel)
        {
            var item = productModel.ToEntity<ProductManufacturer>();
            await _manufacturerService.InsertProductManufacturer(item);           
            return Ok(item);
        }
        [HttpPost]
        [Route("DeleteProductManufacturer")]
        public async Task<ActionResult> DeleteProductManufacturer([FromBody] ProductManufacturerModel productModel)
        {
            var productManufacture =   _manufacturerService.GetProductManufacturersByProductId(productModel.ProductId).FirstOrDefault(x => x.ManufacturerId == productModel.ManufacturerId);
            if (productManufacture == null)
                return NotFound();
            
            await _manufacturerService.DeleteProductManufacturer(productManufacture);
            return Ok();
        }
        #endregion
        // PUT api/<ValuesController>/5
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductModel productModel)
        {
            var product = productModel.ToEntity<Product>();
            await _productService.UpdateProduct(product);
            var productItem = _productCatalogFactory.PrepareProductModel(null, product);
            return Ok(productItem);

        }

        // DELETE api/<ValuesController>/5
        [HttpGet]
        [Route("DeleteProduct/{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            await _productService.DeleteProduct(product);
            var productItem = _productCatalogFactory.PrepareProductModel(null, product);
            return Ok(productItem);
        }

    }
}
