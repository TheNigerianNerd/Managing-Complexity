using TDList.Classes;
namespace TDList.Contracts;

public interface IDataSource
{
    void Create();
    void InsertData();
    List<ToDo> Read();
    bool Update(Guid id, bool isComplete);
    bool Add(IToDoBuilder builder);
}