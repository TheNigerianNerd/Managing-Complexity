using System;
using TDList.Contracts;

namespace TDList.Classes;
public class ToDo : IToDo
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public DateTime? DateLogged { get; init; }
    public bool? IsComplete { get; set; }

    public ToDo(){}

    public ToDo(Guid id, string? title, string? description, DateTime? dateLogged, bool? isComplete)
    {
        Id = id;
        Title = title;
        Description = description;
        DateLogged = dateLogged;
        IsComplete = isComplete;
    }

    public ToDo? GetToDo(Guid id)
    {
        return Id == id ? this : null;
    }

    public ToDo SetToDo(Guid id)
    {
        throw new NotImplementedException();
    }
}
