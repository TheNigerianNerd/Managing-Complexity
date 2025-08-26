using System.Collections.ObjectModel;
using FactoryPattern.Domain;
using FactoryPattern.Interfaces;

namespace FactoryPattern.Factories;

/// <summary>
/// Factory that builds standard burgers from recipe names.
/// DI-friendly and testable (implements IBurgerdFactory).
/// </summary>
public sealed class BurgerFactory : IBurgerFactory
{
    private readonly IReadOnlyDictionary<string, IIngredient> _ingredients;
    private readonly IReadOnlyDictionary<string, Recipe> _recipes;

    public BurgerFactory()
    {
        // In-memory catalog (could be injected later from JSON/DB/config).
        _ingredients = new ReadOnlyDictionary<string, IIngredient>(
            new Dictionary<string, IIngredient>(StringComparer.OrdinalIgnoreCase)
            {
                ["bun"]     = new Ingredient("bun",     "Brioche Bun",     1.00m),
                ["beef"]    = new Ingredient("beef",    "Beef Patty 120g", 2.80m),
                ["chicken"] = new Ingredient("chicken", "Chicken Patty",   2.50m),
                ["veggie"]  = new Ingredient("veggie",  "Veggie Patty",    2.40m),
                ["cheese"]  = new Ingredient("cheese",  "Cheddar Slice",   0.60m),
                ["lettuce"] = new Ingredient("lettuce", "Crisp Lettuce",   0.30m),
                ["tomato"]  = new Ingredient("tomato",  "Tomato Slices",   0.35m),
                ["onion"]   = new Ingredient("onion",   "Red Onion",       0.25m),
                ["pickles"] = new Ingredient("pickles", "Pickles",         0.20m),
                ["ketchup"] = new Ingredient("ketchup", "Ketchup",         0.10m),
                ["mayo"]    = new Ingredient("mayo",    "Mayonnaise",      0.15m),
                ["bbq"]     = new Ingredient("bbq",     "BBQ Sauce",       0.20m),
            });

        _recipes = new ReadOnlyDictionary<string, Recipe>(
            new Dictionary<string, Recipe>(StringComparer.OrdinalIgnoreCase)
            {
                ["Classic"]       = new(new[] { "bun","beef","cheese","lettuce","tomato","onion","ketchup","mayo","bun" }, 0.18m),
                ["Cheese Lover"]  = new(new[] { "bun","beef","cheese","cheese","pickles","ketchup","bun" },                0.20m),
                ["Chicken BBQ"]   = new(new[] { "bun","chicken","cheese","lettuce","bbq","bun" },                          0.17m),
                ["Vegan Delight"] = new(new[] { "bun","veggie","lettuce","tomato","onion","pickles","ketchup","bun" },     0.16m),
            });
    }

    public IEnumerable<string> RecipeNames => _recipes.Keys;
    public IEnumerable<IIngredient> Catalog => _ingredients.Values;

    public IBurger Create(string recipeName)
    {
        if (string.IsNullOrWhiteSpace(recipeName))
            throw new ArgumentException("Recipe name is required.", nameof(recipeName));

        if (!_recipes.TryGetValue(recipeName, out var recipe))
            throw new KeyNotFoundException($"Recipe '{recipeName}' was not found.");

        var parts = ResolveIngredients(recipeName, recipe.IngredientIds);
        var price = CalculatePrice(parts, recipe.Markup);
        return new Burger(recipeName, parts, price);
    }

    public bool TryCreate(string recipeName, out IBurger? burger)
    {
        burger = null;

        if (string.IsNullOrWhiteSpace(recipeName) || !_recipes.TryGetValue(recipeName, out var recipe))
            return false;

        var (ok, parts) = TryResolveIngredients(recipe.IngredientIds);
        if (!ok) return false;

        var price = CalculatePrice(parts!, recipe.Markup);
        burger = new Burger(recipeName, parts!, price);
        return true;
    }

    // --- helpers ---

    private static decimal CalculatePrice(IReadOnlyList<IIngredient> parts, decimal markup)
    {
        var baseTotal = parts.Sum(i => i.Price);
        return Math.Round(baseTotal * (1 + markup), 2, MidpointRounding.AwayFromZero);
    }

    private IReadOnlyList<IIngredient> ResolveIngredients(string recipeName, IReadOnlyList<string> ids)
    {
        var list = new List<IIngredient>(ids.Count);
        foreach (var id in ids)
        {
            if (!_ingredients.TryGetValue(id, out var ing))
                throw new KeyNotFoundException($"Recipe '{recipeName}' references missing ingredient id '{id}'.");
            list.Add(ing);
        }
        return list.AsReadOnly();
    }

    private (bool ok, IReadOnlyList<IIngredient>? parts) TryResolveIngredients(IReadOnlyList<string> ids)
    {
        var list = new List<IIngredient>(ids.Count);
        foreach (var id in ids)
        {
            if (!_ingredients.TryGetValue(id, out var ing))
                return (false, null);
            list.Add(ing);
        }
        return (true, list.AsReadOnly());
    }
}
