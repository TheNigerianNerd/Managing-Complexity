using TDList.Classes;

namespace TDList.Contracts;

public interface IToDoBuilder
{
    IToDoBuilder WithId(Guid identifier);
    IToDoBuilder WithTitle(string title);
    IToDoBuilder WithDescription(string description);
    IToDoBuilder WithDateLogged(DateTime dateLogged);
    IToDoBuilder WithIsComplete(bool isComplete);
    IToDo Build();
    }