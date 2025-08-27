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
            Console.WriteLine("Create a To-Do");
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
            Console.WriteLine("Quit");
            return;
        default:
            Console.WriteLine("Invalid input. Please try again.");
            break;
    }
}

