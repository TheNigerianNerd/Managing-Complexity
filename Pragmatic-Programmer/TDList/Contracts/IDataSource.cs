using TDList.Classes;
namespace TDList.Contracts;

public interface IDataSource
{
    bool Create();
    void InsertData();
    List<ToDo> Read();
    bool Update(Guid id, bool isComplete);
}