var classic = BurgerFactory.Create("Classic");
Print(classic);

var cheese = BurgerFactory.Create("Cheese Lover");
Print(cheese);

static void Print(Burger b)
{
    Console.WriteLine(b);
    foreach (var i in b.Ingredients) Console.WriteLine($"  - {i}");
    Console.WriteLine();
}