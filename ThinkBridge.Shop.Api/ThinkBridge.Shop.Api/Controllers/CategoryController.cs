using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThinkBridge.Shop.Api.Mapper;
using ThinkBridge.Shop.Api.ViewModel.Catalog;
using ThinkBridge.Shop.Core.Domain.Catalog;
using ThinkBridge.Shop.Services;
using ThinkBridge.Shop.Services.Media;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThinkBridge.Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IPictureService _pictureService;
        public CategoryController(ICategoryService categoryService, IPictureService pictureService)
        {
            _categoryService = categoryService;
            _pictureService = pictureService;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categoryModels = new List<CategoryModel>();
             await Task.Run(() =>
             {
                 var categories = _categoryService.GetAllCategories();
                 foreach (var item in categories)
                 {
                     var modelItem = item.ToModel<CategoryModel>();
                     modelItem.PictureUrl = _pictureService.GetPictureUrl(item.PictureId);
                     categoryModels.Add(modelItem);
                 }
             });
            if (categoryModels == null || categoryModels.Count == 0)
                return NoContent();
            return Ok(categoryModels);
           
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _categoryService.GetCategoryById(id);
            var modelItem = item.ToModel<CategoryModel>();
            modelItem.PictureUrl = _pictureService.GetPictureUrl(item.PictureId);

            if (modelItem == null)
                return NotFound();
            return Ok(modelItem);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryModel category)
        {
            var catItem = category.ToEntity<Category>();
            await _categoryService.InsertCategory(catItem);
            return Ok(catItem);
        }

        // PUT api/<CategoryController>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CategoryModel category)
        {
            var catItem = category.ToEntity<Category>();
            await  _categoryService.UpdateCategory(catItem);
            return Ok(catItem);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var catItem = await _categoryService.GetCategoryById(id);
            await _categoryService.DeleteCategory(catItem);
            return Ok();
        }
    }
}
