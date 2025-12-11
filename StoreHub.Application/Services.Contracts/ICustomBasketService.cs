using StoreHub.Application.Dtos.CustomBasket;
namespace StoreHub.Application.Services.Contracts
{
    public interface ICustomBasketService
    {
        Task<CustomBasketDto> GetBasketAsync(string id);
        Task<CustomBasketDto> UpdateBasketAsync(CustomBasketDto basket);
        Task<bool> DeleteBasketAsync(string id);
    }
}
