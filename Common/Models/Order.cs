using Common.Interfaces;

namespace Common.Models
{
    public class Order : IOrderForDelivery
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
}