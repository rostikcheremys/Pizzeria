using Newtonsoft.Json;

namespace Pizzeria;

public class DeliveryInfo
{
    public string Name { get; init; }
    public string Phone { get; init; }
    public string? DeliveryOption { get; init; }
    public string City { get; init; }
    public string Address { get; init; }
    public DateTime DeliveryDate { get; init; }
    public string DeliveryTime { get; init; }
    public List<OrderInfo> OrderInfo { get; init; } = new();
}

public class OrderInfo
{
    [JsonProperty(Order = 0)]
    public string? ProductName { get; init; }
    
    [JsonProperty(Order = 1)]
    public string? ProductSize { get; init; }
    
    [JsonProperty(Order = 3)]
    public int ProductQuantity { get; init; }
    
    [JsonProperty(Order = 4)]
    public double ProductPrice { get; init; }
}

public class PizzaDetails : OrderInfo
{
    [JsonProperty(Order = 2)]
    public List<string?> ProductToppings { get; init; } = new();
}

public class DrinkDetails : OrderInfo;