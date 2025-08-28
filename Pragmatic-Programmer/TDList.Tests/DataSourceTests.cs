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
        private readonly IDataSource _JSONdataSource;

        public DataSourceTests()
        {
            _dataSource = new FakeDataSource();
            _JSONdataSource = new DataSource();
        }


        [Fact]
        public void Exists_WithValidConnection_ReturnsTrue()
        {
            Assert.True(_dataSource.Exists("good-connection"));
        }
        [Fact]
        public void Exists_WithJSonConnection_ReturnsFalse()
        {
            Assert.False(_JSONdataSource.Exists("SampleConnection"));
        }

        [Fact]
        public void Exists_WithInvalidConnection_ReturnsFalse()
        {
            Assert.False(_dataSource.Exists("bad-connection"));
        }

        [Fact]
        public void IsValid_WithGoodPrefix_ReturnsTrue()
        {
            Assert.True(_dataSource.IsValid("good-123"));
        }

        [Fact]
        public void IsValid_WithoutGoodPrefix_ReturnsFalse()
        {
            Assert.False(_dataSource.IsValid("bad-123"));
        }

        [Fact]
        public void Get_WithValidConnection_ReturnsTodos()
        {
            var todos = _dataSource.Get("good-connection").ToList();
            Assert.NotEmpty(todos);
            Assert.Equal(2, todos.Count);
        }

        [Fact]
        public void Get_WithInvalidConnection_Throws()
        {
            Assert.Throws<InvalidOperationException>(() => _dataSource.Get("invalid"));
        }
    }
}
