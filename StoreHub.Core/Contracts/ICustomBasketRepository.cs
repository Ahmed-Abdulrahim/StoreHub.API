using Domain.Models;
namespace Domain.Contracts
{
    public interface ICustomBasketRepository
    {
        Task<CustomBasket> GetBasketAsync(string id);
        Task<CustomBasket> UpdateBasketAsync(CustomBasket model, TimeSpan? timeToLive = null);
        Task<bool> DeleteBasket(string id);
    }
}
