using TDList.Contracts;
using TDList.Models;
using TDList.Classes;

namespace TDList.Tests.Fakes
{
    public class FakeDataSource : IDataSource
    {
        private readonly List<ToDo> _items;
        private readonly HashSet<string> _validConnections;

        public FakeDataSource()
        {
            _items = new List<ToDo>
            {
                new ToDo(Guid.NewGuid(), "Test Title 1", "Test Desc 1", DateTime.Now, false),
                new ToDo(Guid.NewGuid(), "Test Title 2", "Test Desc 2", DateTime.Now, true)
            };

            _validConnections = new HashSet<string> { "good-connection" };
        }

        public bool Exists(string connection) => _validConnections.Contains(connection);

        public bool IsValid(string connection) => connection.StartsWith("good");

        public IEnumerable<ToDo> Get(string connection)
        {
            if (!IsValid(connection))
                throw new InvalidOperationException("Invalid connection string.");
            return _items;
        }
    }
}
