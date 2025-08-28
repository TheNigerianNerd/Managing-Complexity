using TDList.Classes;

namespace TDList.Contracts;
    public interface IToDo
    {
        Guid Id { get; }
        string? Title { get; }
        string? Description { get; }
        DateTime? DateLogged { get; }
        bool? IsComplete { get; }
    }


