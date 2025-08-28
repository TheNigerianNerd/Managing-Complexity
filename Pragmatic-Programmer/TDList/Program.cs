// See https://aka.ms/new-console-template for more information
using TDList.Contracts;
using TDList.Classes;
using TDList.Models;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

Console.WriteLine("Welcome to the To-Do List!");

while (true)
{
    Console.WriteLine("Please select an option:");
    int choice = int.MinValue;
    UserChoice Choice = new UserChoice();

    while (!int.TryParse(Console.ReadLine(), out choice) || !Choice.IsValid(choice))
    {
        Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
    }
    
    switch ((Choices)choice)
    {
        case Choices.CreateTD:
            CreateToDo();

            break;
        case Choices.ReadTD:
            Console.WriteLine("Read a To-Do");
            break;
        case Choices.UpdateTD:
            Console.WriteLine("Update a To-Do");
            break;
        case Choices.DeleteTD:
            Console.WriteLine("Delete a To-Do");
            break;
        case Choices.QuitTD:
            Console.WriteLine("Goodbye!");
            return;
    }
}

void CreateToDo()
{
    Console.WriteLine("Create a To-Do");
    Console.Write("Enter title: ");
    string? title = Console.ReadLine();
    Console.Write("Enter description: ");
    string? description = Console.ReadLine();
    Console.Write("Enter date logged (MM/dd/yyyy): ");
    DateTime dateLogged = DateTime.MinValue;
    if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
    {
        dateLogged = date;
    }
    
    Console.Write("Is complete? (y/n): ");
    bool isComplete = false;
    var response = Console.ReadLine()?.ToLower();
    if (response == "y")
    {
        isComplete = true;
    }
    else if (Console.ReadLine().ToLower() == "n")
    {
        isComplete = false;
    }
    var todo = new ToDoBuilder()
        .WithTitle(title)
        .WithDescription(description)
        .WithDateLogged(dateLogged)
        .WithIsComplete(isComplete)
        .Build();
}

