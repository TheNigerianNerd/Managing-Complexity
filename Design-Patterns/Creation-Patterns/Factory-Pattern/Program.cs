using FactoryPattern.Factories;
using FactoryPattern.Interfaces;

IBurgerFactory factory = new BurgerFactory();

var classic = factory.Create("Classic");
Console.WriteLine($"Built: {classic.Name} - ${classic.Price:F2}");
