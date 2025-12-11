using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using StoreHub.Application.Dtos.CustomBasket;
using StoreHub.Application.Services.Contracts;
namespace StoreHub.Application.Services
{
    public class CustomBasketService(ICustomBasketRepository basketRepository, IMapper mapper) : ICustomBasketService
    {

        public async Task<CustomBasketDto> GetBasketAsync(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            if (basket is null) throw new Exception("Basket not found");
            var result = mapper.Map<CustomBasketDto>(basket);
            return result;
        }

        public async Task<CustomBasketDto> UpdateBasketAsync(CustomBasketDto basket)
        {
            var baseBasket = mapper.Map<CustomBasket>(basket);
            var result = await basketRepository.UpdateBasketAsync(baseBasket);
            if (result is null) throw new Exception("Failed to update basket");
            var dtoResult = mapper.Map<CustomBasketDto>(result);
            return dtoResult;
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            var flag = await basketRepository.DeleteBasket(id);
            if (flag == false) throw new Exception("Failed to delete basket");
            return flag;
        }

    }
}
