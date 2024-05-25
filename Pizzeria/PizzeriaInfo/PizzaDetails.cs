using Newtonsoft.Json;

namespace Pizzeria.PizzeriaInfo
{
    public class PizzaDetails : OrderInfo
    {
        [JsonProperty(Order = 2)]
        public List<string> ProductToppings { get; init; } = new();
    }
}