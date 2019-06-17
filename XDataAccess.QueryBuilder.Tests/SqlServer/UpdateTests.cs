using XDataAccess.QueryBuilder.Compilers.Databases;
using XDataAccess.QueryBuilder.Tests.TestModels;
using Xunit;

namespace XDataAccess.QueryBuilder.Tests.SqlServer
{
    public class UpdateTests
    {
        [Fact]
        public void Update_WithIdentity_Test()
        {
            var compiler = new SqlServerCompiler();
            var builder = new QueryBuilder<EmployeeWithAttributes>(compiler);
            var employeeToUpdate = new EmployeeWithAttributes() { Id = 1, Name = "test" };

            var result = builder.Update(employeeToUpdate) as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(2, result.QueryParameters.Count);
            Assert.Equal("test", result.QueryParameters["@P0"]);
            Assert.Equal(1, result.QueryParameters["@P1"]);
            Assert.Equal("UPDATE dbo.employee SET [name] = @P0 WHERE [Id] = @P1", result.SqlQuery);
        }

        [Fact]
        public void Delete_WithIdentityAndNullProperty_Test()
        {
            var compiler = new SqlServerCompiler();
            var builder = new QueryBuilder<EmployeeWithAttributes>(compiler);
            var employeeToUpdate = new EmployeeWithAttributes() { Id = 1 };

            var result = builder.Update(employeeToUpdate) as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(2, result.QueryParameters.Count);
            Assert.Null(result.QueryParameters["@P0"]);
            Assert.Equal(1, result.QueryParameters["@P1"]);
            Assert.Equal("UPDATE dbo.employee SET [name] = @P0 WHERE [Id] = @P1", result.SqlQuery);
        }

        [Fact]
        public void Delete_WithWhereExpression_Test()
        {
            var compiler = new SqlServerCompiler();
            var builder = new QueryBuilder<EmployeeWithAttributes>(compiler);
            var employeeToUpdate = new EmployeeWithAttributes() { Name = "test" };

            var result = builder.Update(employeeToUpdate, e => e.Id == 1) as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(2, result.QueryParameters.Count);
            Assert.Equal("test", result.QueryParameters["@P0"]);
            Assert.Equal(1, result.QueryParameters["@P1"]);
            Assert.Equal("UPDATE dbo.employee SET [name] = @P0 WHERE ([Id] = @P1)", result.SqlQuery);
        }
    }
}