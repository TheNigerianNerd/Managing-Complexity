using TDList.Contracts;
using TDList.Data;
using TDList.Models;
using TDList.Tests.Fakes;
using Xunit;

namespace TDList.Tests
{
    public class DataSourceTests
    {
        private readonly IDataSource _dataSource;
        

        public DataSourceTests()
        {
            _dataSource = new FakeDataSource();
        }
        [Fact]
        public void Exists_WithValidConnection_ReturnsTrue()
        {
            Assert.True(_dataSource.Exists("good-connection"));
        }
    }
}
