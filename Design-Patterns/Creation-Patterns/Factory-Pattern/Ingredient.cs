public sealed class Ingredient
    {
        public Ingredient(string id, string name, decimal price)
        { Id = id; Name = name; Price = price; }

        public string Id { get; }
        public string Name { get; }
        public decimal Price { get; }
        public override string ToString() => $"{Name} (${Price:0.00})";
    }