namespace Pizzeria;

public static class PizzaData
{
    public static readonly List<PizzaDetails> Pizza =
    [
        new PizzaDetails("Pepperoni", "images/pizza/pepperoni.png", "Pepperoni, Chili peppers, Cheese, Tomato Sauce", 15),
        new PizzaDetails("Sicilian", "images/pizza/sicilian.png", "Pepperoni, Olives, Tomatoes, Peppers, Cheese", 12),
        new PizzaDetails("Hawaiian", "images/pizza/hawaiian.png", "Mozzarella, Chicken, Pineapple, Tomato Sauce", 11),
        new PizzaDetails("Four Cheese", "images/pizza/four-cheese.png", "Mozzarella, Parmesan, Feta, Gorgonzola", 15),
        new PizzaDetails("Prosciutto e Funghi", "images/pizza/prosciutto-e-funghi.png", "Ham, Mushrooms, Cheese, Tomato Sauce", 13),
        new PizzaDetails("Chicken with Mushrooms", "images/pizza/сhicken-with-mushrooms.png", "Chicken, Mushrooms, Tomatoes, Cheese", 12)
    ];
}