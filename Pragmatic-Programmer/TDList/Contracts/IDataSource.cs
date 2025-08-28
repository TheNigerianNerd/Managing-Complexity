using TDList.Classes;
namespace TDList.Contracts;

public interface IDataSource
{
    bool Exists(string connection);
    bool IsValid(string connection);
    IEnumerable<ToDo> Get(string connection);
    //Create Data Source if none exist
    /*string Create()
    {

    }*/
}