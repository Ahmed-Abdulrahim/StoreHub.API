namespace StoreHub.Application.Dtos.CustomBasket
{
    public class CustomBasketDto
    {
        public string Id { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; }
    }
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PuctureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
