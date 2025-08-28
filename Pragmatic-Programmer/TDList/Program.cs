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
    string title = "Build a todo list";
    string description = "Build a todo list";
    DateTime dateLogged = new DateTime(1991, 5, 19);
    bool isComplete = false;

    var todo = new ToDoBuilder()
        .WithTitle(title)
        .WithDescription(description)
        .WithDateLogged(dateLogged)
        .WithIsComplete(isComplete)
        .Build();

    Console.WriteLine($"Id: {todo.Id}");
    Console.WriteLine($"Title: {todo.Title}");
    Console.WriteLine($"Description: {todo.Description}");
    Console.WriteLine($"Date Logged: {todo.DateLogged}");
    Console.WriteLine($"Is Complete: {todo.IsComplete}");
}



