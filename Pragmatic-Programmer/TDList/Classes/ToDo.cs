using System;
using TDList.Contracts;

namespace TDList.Classes;
public class ToDo : IToDo
{
    private int? id;
    private string? title;
    private string? description;
    private DateTime? dateLogged;
    private bool? isComplete;

    public ToDo(){}

    public ToDo(int? id, string? title, string? description, DateTime? dateLogged, bool? isComplete)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.dateLogged = dateLogged;
        this.isComplete = isComplete;
    }

    public ToDo getToDo(int id)
    {
        if(this.id != id) throw new ArgumentException($"ToDo with id: {id} does not exist.");
        
        return this;
    }
    public ToDo setToDo()
    {
        throw new NotImplementedException();
    }
}