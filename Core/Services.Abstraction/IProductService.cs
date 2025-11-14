using Shared;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IProductService
    {
        Task<PaginateResponse<ProductDto>> GetAllProductAsync(int? brandId, int? typeId, string? sort, int? pageIndex, int? pageSize);
        //GetById
        Task<ProductDto> GetProductById(int id);
        //GetAll Brand
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        //GetAll Types
        Task<IEnumerable<TypeDto>> GetAllTypes();
    }
}
