using TDList.Classes;

namespace TDList.Contracts;

public interface IToDo
{
    ToDo getToDo(int identifier);
    ToDo setToDo();
}