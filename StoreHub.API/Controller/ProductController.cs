using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using Shared.Dtos;
using StoreHub.API.Errors;
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
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductRequestDto model)
        {
            var models = await servicesManager.IProductService.GetAllProductAsync(model);
            if (models is null) return BadRequest(new ApiResponse(400));
            return Ok(models);
        }
        //GetById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await servicesManager.IProductService.GetProductById(id);
            //if (model is null) return NotFound(new ApiResponse(404));
            return Ok(model);

        }

        //GetAllBrands
        [HttpGet("GetBrands")]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await servicesManager.IProductService.GetAllBrandsAsync();
            if (brands is null) return BadRequest(new ApiResponse(400));
            return Ok(brands);
        }

        //GetAll Types
        [HttpGet("GetTypes")]
        public async Task<IActionResult> GetTypes()
        {
            var types = await servicesManager.IProductService.GetAllTypes();
            if (types is null) return BadRequest(new ApiResponse(400));
            return Ok(types);
        }


        //
        [HttpGet("GetError/{id}")]
        public async Task<IActionResult> GetError(int id)
        {
            throw new Exception();
        }


    }
}
