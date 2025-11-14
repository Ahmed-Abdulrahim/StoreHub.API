using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IServiceManager servicesManager) : ControllerBase
    {
        //GetAllProduct

        //sort
        //nameasc
        //namedesc
        //priceasc
        //pricedesc
        [HttpGet]
        public async Task<IActionResult> GetAllProducts(int? brandId, int? typeId, string? sort, int? pageIndex, int? pageSize)
        {
            var models = await servicesManager.IProductService.GetAllProductAsync(brandId, typeId, sort, pageIndex, pageSize);
            if (models is null) return BadRequest();
            return Ok(models);
        }
        //GetById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await servicesManager.IProductService.GetProductById(id);
            if (model is null) return NotFound();
            return Ok(model);

        }

        //GetAllBrands
        [HttpGet("GetBrands")]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await servicesManager.IProductService.GetAllBrandsAsync();
            if (brands is null) return BadRequest();
            return Ok(brands);
        }

        //GetAll Types
        [HttpGet("GetTypes")]
        public async Task<IActionResult> GetTypes()
        {
            var types = await servicesManager.IProductService.GetAllTypes();
            if (types is null) return BadRequest();
            return Ok(types);
        }


    }
}
