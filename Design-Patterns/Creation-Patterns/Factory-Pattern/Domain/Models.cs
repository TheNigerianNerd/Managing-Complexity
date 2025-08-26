using System.Collections.ObjectModel;
using FactoryPattern.Interfaces;

namespace FactoryPattern.Domain;

// Immutable, sealed implementations of the interfaces.
public sealed class Ingredient : IIngredient
{
    public Ingredient(string id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public string Id { get; }
    public string Name { get; }
    public decimal Price { get; }
}

public sealed class Burger : IBurger
{
    public Burger(string name, IReadOnlyList<IIngredient> parts, decimal price)
    {
        Name = name;
        Parts = parts;
        Price = price;
    }

    public string Name { get; }
    public IReadOnlyList<IIngredient> Parts { get; }
    public decimal Price { get; }
}

public sealed record Recipe(IReadOnlyList<string> IngredientIds, decimal Markup);