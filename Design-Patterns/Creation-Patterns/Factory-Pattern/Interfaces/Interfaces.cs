namespace FactoryPattern.Interfaces;

public interface IIngredient
{
    string Id { get; }
    string Name { get; }
    decimal Price { get; }
}

public interface IBurger
{
    string Name { get; }
    IReadOnlyList<IIngredient> Parts { get; }
    decimal Price { get; }
}

public interface IBurgerFactory
{
    IBurger Create(string recipeName);
    bool TryCreate(string recipeName, out IBurger? burger);

    // Optional discovery helpers (kept read-only)
    IEnumerable<string> RecipeNames { get; }
    IEnumerable<IIngredient> Catalog { get; }
}
