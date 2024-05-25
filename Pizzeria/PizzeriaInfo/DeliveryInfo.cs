namespace Pizzeria.PizzeriaInfo
{
    public class DeliveryInfo
    {
        public DateTime OrderDate { get; init; }
        public TimeSpan OrderTime { get; init; }
        public string? Name { get; init; }
        public string? Phone { get; init; }
        public string? DeliveryOption { get; init; }
        public string? City { get; init; }
        public string? Address { get; init; }
        public DateTime DeliveryDate { get; init; }
        public string? DeliveryTime { get; init; }
        public double? TotalPrice { get; set; }
        public List<OrderInfo> OrderInfo { get; init; } = new();
    }
}