public sealed class Burger
    {
        public Burger(string name, IReadOnlyList<Ingredient> ingredients, decimal price)
        { Name = name; Ingredients = ingredients; Price = price; }

        public string Name { get; }
        public IReadOnlyList<Ingredient> Ingredients { get; }
        public decimal Price { get; }
        public override string ToString() => $"{Name}: ${Price:0.00}";
    }