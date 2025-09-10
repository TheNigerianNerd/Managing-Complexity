using TDList.Classes;
namespace TDList.Contracts;

public interface IDataSource
{
    bool Create();
    bool Read();
}