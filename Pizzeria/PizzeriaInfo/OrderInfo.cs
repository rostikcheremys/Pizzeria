using Newtonsoft.Json;

namespace Pizzeria.PizzeriaInfo
{
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
}

