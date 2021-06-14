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
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;
        private readonly IPictureService _pictureService;
        public ManufacturerController(IManufacturerService manufacturerService,
            IPictureService pictureService )
        {
            _manufacturerService = manufacturerService;
            _pictureService = pictureService;
        }
        // GET: api/<ManufacturerController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var manufacturerModels = new List<ManufacturerModel>();
            await Task.Run(() =>
            {
                var manufacturers = _manufacturerService.GetAllManufacturer();
                foreach (var item in manufacturers)
                {
                    var modelItem = item.ToModel<ManufacturerModel>();
                    modelItem.PictureUrl = _pictureService.GetPictureUrl(item.PictureId);
                    manufacturerModels.Add(modelItem);
                }
            }); if (manufacturerModels == null || manufacturerModels.Count == 0)
                return NoContent();
            return Ok(manufacturerModels);
        }

        // GET api/<ManufacturerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _manufacturerService.GetManufacturerById(id);
            var modelItem = item.ToModel<ManufacturerModel>();
            modelItem.PictureUrl = _pictureService.GetPictureUrl(item.PictureId);

            if (modelItem == null)
                return NotFound();
            return Ok(modelItem);
        }

        // POST api/<ManufacturerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ManufacturerModel manufacturer)
        {
            var manItem = manufacturer.ToEntity<Manufacturer>();
            await _manufacturerService.InsertManufacturer(manItem);
            return Ok(manItem);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ManufacturerModel manufacturer)
        {
            var manItem = manufacturer.ToEntity<Manufacturer>();
            await _manufacturerService.UpdateManufacturer(manItem);
            return Ok(manItem);
        }

        // DELETE api/<ManufacturerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var manItem = await _manufacturerService.GetManufacturerById(id);
            await _manufacturerService.DeleteManufacturer(manItem);
            return Ok(manItem);
        }
    }
}
