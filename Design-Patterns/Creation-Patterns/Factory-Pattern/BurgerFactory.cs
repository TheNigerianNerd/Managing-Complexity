using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

// === Domain Records ===
// Records keep things immutable by default and are great for value-like data.
public sealed record Ingredient(string Id, string Name, decimal Price);
public sealed record Burger(string Name, IReadOnlyList<Ingredient> Parts, decimal Price);

// Group recipe details in one place for clarity.
public sealed record Recipe(IReadOnlyList<string> IngredientIds, decimal Markup);

// === Factory ===
// NOTE (Factory Pattern): The caller asks for a Burger by a high-level name ("Classic")
// and the factory hides the assembly details (which parts + how to price).
// Benefit: Consistent construction, fewer invalid objects in the system.
// Tradeoff: Indirection—requires reading factory code when you want to see what’s inside.
public static class BurgerFactory
{
    // Catalog: ingredient-id -> ingredient
    // NOTE: StringComparer.OrdinalIgnoreCase avoids callers caring about casing.
    private static readonly IReadOnlyDictionary<string, Ingredient> Ingredients =
        new ReadOnlyDictionary<string, Ingredient>(
            new Dictionary<string, Ingredient>(StringComparer.OrdinalIgnoreCase)
            {
                ["bun"]     = new("bun",     "Brioche Bun",     1.00m),
                ["beef"]    = new("beef",    "Beef Patty 120g", 2.80m),
                ["chicken"] = new("chicken", "Chicken Patty",   2.50m),
                ["veggie"]  = new("veggie",  "Veggie Patty",    2.40m),
                ["cheese"]  = new("cheese",  "Cheddar Slice",   0.60m),
                ["lettuce"] = new("lettuce", "Crisp Lettuce",   0.30m),
                ["tomato"]  = new("tomato",  "Tomato Slices",   0.35m),
                ["onion"]   = new("onion",   "Red Onion",       0.25m),
                ["pickles"] = new("pickles", "Pickles",         0.20m),
                ["ketchup"] = new("ketchup", "Ketchup",         0.10m),
                ["mayo"]    = new("mayo",    "Mayonnaise",      0.15m),
                ["bbq"]     = new("bbq",     "BBQ Sauce",       0.20m),
            });

    // Recipes: burger-name -> recipe
    private static readonly IReadOnlyDictionary<string, Recipe> Recipes =
        new ReadOnlyDictionary<string, Recipe>(
            new Dictionary<string, Recipe>(StringComparer.OrdinalIgnoreCase)
            {
                ["Classic"]       = new(new[] { "bun","beef","cheese","lettuce","tomato","onion","ketchup","mayo","bun" }, 0.18m),
                ["Cheese Lover"]  = new(new[] { "bun","beef","cheese","cheese","pickles","ketchup","bun" },                0.20m),
                ["Chicken BBQ"]   = new(new[] { "bun","chicken","cheese","lettuce","bbq","bun" },                          0.17m),
                ["Vegan Delight"] = new(new[] { "bun","veggie","lettuce","tomato","onion","pickles","ketchup","bun" },     0.16m),
            });

    /// <summary>
    /// Build a Burger from a named recipe or throw with a precise message.
    /// </summary>
    public static Burger Create(string recipeName)
    {
        if (string.IsNullOrWhiteSpace(recipeName))
            throw new ArgumentException("Recipe name is required.", nameof(recipeName));

        if (!Recipes.TryGetValue(recipeName, out var recipe))
            throw new KeyNotFoundException($"Recipe '{recipeName}' was not found.");

        // Map ingredient ids -> Ingredient instances & validate as we go for better error messages.
        var parts = ResolveIngredients(recipeName, recipe.IngredientIds);

        var price = CalculatePrice(parts, recipe.Markup);
        return new Burger(recipeName, parts, price);
    }

    /// <summary>
    /// Try-pattern to avoid exceptions if that fits a calling context better.
    /// </summary>
    public static bool TryCreate(string recipeName, out Burger? burger)
    {
        burger = null;

        if (string.IsNullOrWhiteSpace(recipeName) || !Recipes.TryGetValue(recipeName, out var recipe))
            return false;

        // If any ingredient fails to resolve, consider this a failure (no exceptions in Try pattern).
        var (ok, parts) = TryResolveIngredients(recipe.IngredientIds);
        if (!ok) return false;

        var price = CalculatePrice(parts!, recipe.Markup);
        burger = new Burger(recipeName, parts!, price);
        return true;
    }

    // ---- Helpers ----

    // Turn ids into concrete Ingredients with crisp validation.
    private static IReadOnlyList<Ingredient> ResolveIngredients(string recipeName, IReadOnlyList<string> ids)
    {
        var list = new List<Ingredient>(capacity: ids.Count);

        foreach (var id in ids)
        {
            if (!Ingredients.TryGetValue(id, out var ingredient))
                throw new KeyNotFoundException($"Recipe '{recipeName}' references missing ingredient id '{id}'.");

            list.Add(ingredient);
        }

        return list.AsReadOnly();
    }

    // Non-throwing version for TryCreate
    private static (bool ok, IReadOnlyList<Ingredient>? parts) TryResolveIngredients(IReadOnlyList<string> ids)
    {
        var list = new List<Ingredient>(capacity: ids.Count);

        foreach (var id in ids)
        {
            if (!Ingredients.TryGetValue(id, out var ingredient))
                return (false, null);

            list.Add(ingredient);
        }

        return (true, list.AsReadOnly());
    }

    // Round with explicit mode to keep pricing deterministic.
    private static decimal CalculatePrice(IReadOnlyList<Ingredient> parts, decimal markup)
    {
        var baseTotal = parts.Sum(i => i.Price);
        var price = baseTotal * (1 + markup);
        return Math.Round(price, 2, MidpointRounding.AwayFromZero);
    }

    // Optional: expose read-only views if callers need to list available options.
    public static IEnumerable<string> RecipeNames => Recipes.Keys;
    public static IEnumerable<Ingredient> Catalog => Ingredients.Values;
}

/*
NOTES (Pragmatic Factory Pattern: Benefits & Tradeoffs)

1) Single Construction Path
   - Benefit: A "Classic" burger is built the same way everywhere. Less drift, fewer invalid states.
   - Tradeoff: If you need many variations (e.g., "no onions"), you’ll either add too many recipes or
               introduce parameters (moving towards a builder).

2) Encapsulation of Rules (pricing/rounding/validation)
   - Benefit: Pricing logic is centralized (markup, rounding mode). Easy to audit and change.
   - Tradeoff: If you need per-order pricing rules (coupons, surge), the factory can grow responsibilities.
               Consider splitting (PricingService, CatalogService) or passing a strategy into the factory.

3) Clear, Testable API
   - Benefit: Create/TryCreate patterns let callers pick exception or boolean flows.
   - Tradeoff: Static factories are less flexible for dependency injection and mocking.
               If you need that, introduce IBurgerFactory and a non-static implementation.

4) IDs vs Strong Types
   - Benefit: Using string IDs keeps data fixtures simple, easy to add from config or JSON.
   - Tradeoff: Magic strings don’t get compiler checks. Mitigations:
               * central catalog (we do this),
               * integration tests against recipe integrity,
               * (Optionally) codegen enums from catalog.

5) Performance Consideration
   - This maps ids => ingredients every call. For huge volume or hot paths, cache the resolved
     ingredients per recipe (i.e., precompile recipes at startup). Keep it simple until profiling says otherwise.

6) Thread-Safety
   - All dictionaries are created once and exposed read-only; factory is effectively stateless => safe for concurrent use.

7) Extensibility
   - If you foresee “build your burger” flows, keep this factory for “named standards” and
     add a BurgerBuilder that takes a sequence of Ingredient objects and composes price via a PricingStrategy.
*/
