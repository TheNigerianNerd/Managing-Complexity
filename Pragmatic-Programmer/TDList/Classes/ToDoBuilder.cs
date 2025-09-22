using TDList.Contracts;
using TDList.Data;

namespace TDList.Classes;

public class ToDoBuilder : IToDoBuilder
{
    private Guid _id;
    private string? _title;
    private string? _description;
    private DateTime? _dateLogged;
    private bool? _isComplete;
    private ConnectionProvider _connectionProvider = ConnectionProvider.Instance;

    public ToDoBuilder() { }

    public IToDoBuilder WithId(Guid identifier)
    {
        _id = identifier;
        return this;
    }

    public IToDoBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public IToDoBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public IToDoBuilder WithDateLogged(DateTime dateLogged)
    {
        _dateLogged = dateLogged;
        return this;
    }

    public IToDoBuilder WithIsComplete(bool isComplete)
    {
        _isComplete = isComplete;
        return this;
    }

    public IToDo Build()
    {
        return new ToDo(_id, _title, _description, _dateLogged, _isComplete);
    }
}
