using TDList.Contracts;
using TDList.Classes;
namespace TDList.Data;
public class DataSource : IDataSource
{
    public bool Exists(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
                return false;

            // Ensure it specifically points to TDList.json
            var filePath = Path.Combine(connection, "TDList.json");
            return File.Exists(filePath);
        }
    public bool IsValid(string connection)
        {

            // For now, let's just piggyback on Exists
            return Exists(connection);
        }

        public IEnumerable<ToDo> Get(string connection)
        {
            var filePath = Path.Combine(connection, "TDList.json");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Data source not found: {filePath}");

            // Later you can add JSON deserialization here
            return new List<ToDo>();
        }
}