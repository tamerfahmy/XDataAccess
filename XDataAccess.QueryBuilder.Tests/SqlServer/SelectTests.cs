using XDataAccess.QueryBuilder.Compilers.Databases;
using XDataAccess.QueryBuilder.Tests.TestModels;
using Xunit;

namespace XDataAccess.QueryBuilder.Tests.SqlServer
{
    public class SelectTests
    {
        [Fact]
        public void Select_WithoutWhereWithoutAttribute_Test()
        {
            var compiler = new SqlServerCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var result = builder.Select() as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(0, result.QueryParameters.Count);
            Assert.Equal("SELECT * FROM Employee", result.SqlQuery);
        }

        [Fact]
        public void Select_WithoutWhereWithAttributes_Test()
        {
            var compiler = new SqlServerCompiler();
            var builder = new QueryBuilder<EmployeeWithAttributes>(compiler);

            var result = builder.Select() as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(0, result.QueryParameters.Count);
            Assert.Equal("SELECT * FROM dbo.employee", result.SqlQuery);
        }

        [Fact]
        public void Select_WithWhereWithAttributes_Test()
        {
            var compiler = new SqlServerCompiler();
            var builder = new QueryBuilder<EmployeeWithAttributes>(compiler);

            var result = builder.Select(e => e.Id == 1) as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(1, result.QueryParameters.Count);
            Assert.Equal(1, result.QueryParameters["@P0"]);
            Assert.Equal("SELECT * FROM dbo.employee WHERE ([Id] = @P0)", result.SqlQuery);
        }
    }
}