using XDataAccess.QueryBuilder.Compilers.Databases;
using XDataAccess.QueryBuilder.Tests.TestData;
using Xunit;

namespace XDataAccess.QueryBuilder.Tests.SqlServer
{
    public class SelectTests
    {
        [Fact]
        public void Select_WithoutWhere_Test()
        {
            var compiler = new SqlServerCompiler();
            var builder = new QueryBuilder<EmployeeWithAttributes>(compiler);
            var employeeToUpdate = new EmployeeWithAttributes() { Id = 1, Name = "test" };

            var result = builder.Select() as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(0, result.QueryParameters.Count);
            Assert.Equal("SELECT * FROM dbo.employee", result.SqlQuery);
        }

        [Fact]
        public void Select_WithWhere_Test()
        {
            var compiler = new SqlServerCompiler();
            var builder = new QueryBuilder<EmployeeWithAttributes>(compiler);
            var employeeToUpdate = new EmployeeWithAttributes() { Id = 1, Name = "test" };

            var result = builder.Select(e => e.Id == 1) as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(1, result.QueryParameters.Count);
            Assert.Equal(1, result.QueryParameters["@P0"]);
            Assert.Equal("SELECT * FROM dbo.employee WHERE ([Id] = @P0)", result.SqlQuery);
        }
    }
}