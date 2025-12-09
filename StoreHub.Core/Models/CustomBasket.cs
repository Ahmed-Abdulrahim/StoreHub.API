namespace Domain.Models
{
    public class CustomBasket
    {
        public int Id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }
    }


    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PuctureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
