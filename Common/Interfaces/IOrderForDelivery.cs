namespace Common.Interfaces
{
    public interface IOrderForDelivery
    {
        int Id { get; set; }
        string Name { get; set; }
        int Quantity { get; set; }
        decimal Price { get; set; }
        DateTime OrderDate { get; set; }
    }
}