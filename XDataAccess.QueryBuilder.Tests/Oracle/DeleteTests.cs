using XDataAccess.QueryBuilder.Compilers.Databases;
using XDataAccess.QueryBuilder.Tests.TestModels;
using Xunit;

namespace XDataAccess.QueryBuilder.Tests.Oracle
{
    public class DeleteTests
    {
        [Fact]
        public void Delete_SimpleEqualCondition_Test()
        {
            var compiler = new OrcaleCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var result = builder.Delete(e => e.Id == 1) as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(1, result.QueryParameters.Count);
            Assert.Equal(1, result.QueryParameters[":P0"]);
            Assert.Equal("DELETE FROM Employee WHERE (\"Id\" = :P0)", result.SqlQuery);
        }

        [Fact]
        public void Delete_SimpleEqualWithAndCondition_Test()
        {
            var compiler = new OrcaleCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var result = builder.Delete(e => e.Id == 1 && e.Name == "test") as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(2, result.QueryParameters.Count);
            Assert.Equal(1, result.QueryParameters[":P0"]);
            Assert.Equal("test", result.QueryParameters[":P1"]);
            Assert.Equal("DELETE FROM Employee WHERE ((\"Id\" = :P0) AND (\"Name\" = :P1))", result.SqlQuery);
        }

        [Fact]
        public void Delete_SimpleEqualWithOrCondition_Test()
        {
            var compiler = new OrcaleCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var result = builder.Delete(e => e.Id == 1 || e.Name == "test") as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(2, result.QueryParameters.Count);
            Assert.Equal(1, result.QueryParameters[":P0"]);
            Assert.Equal("test", result.QueryParameters[":P1"]);
            Assert.Equal("DELETE FROM Employee WHERE ((\"Id\" = :P0) OR (\"Name\" = :P1))", result.SqlQuery);
        }

        [Fact]
        public void Delete_AndWithOrWithGroupingCondition_Test()
        {
            var compiler = new OrcaleCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var result = builder.Delete(e => (e.Id == 1 && e.Name == "value1") || e.Name != "value2") as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(3, result.QueryParameters.Count);
            Assert.Equal(1, result.QueryParameters[":P0"]);
            Assert.Equal("value1", result.QueryParameters[":P1"]);
            Assert.Equal("value2", result.QueryParameters[":P2"]);
            Assert.Equal("DELETE FROM Employee WHERE (((\"Id\" = :P0) AND (\"Name\" = :P1)) OR (\"Name\" <> :P2))", result.SqlQuery);
        }

        [Fact]
        public void Delete_NullCondition_Test()
        {
            var compiler = new OrcaleCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var result = builder.Delete(e => e.Name == null) as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(0, result.QueryParameters.Count);
            Assert.Equal("DELETE FROM Employee WHERE (\"Name\" IS NULL)", result.SqlQuery);
        }

        [Fact]
        public void Delete_NotNullCondition_Test()
        {
            var compiler = new OrcaleCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var result = builder.Delete(e => e.Name != null) as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(0, result.QueryParameters.Count);
            Assert.Equal("DELETE FROM Employee WHERE (\"Name\" IS NOT NULL)", result.SqlQuery);
        }

        [Fact]
        public void Delete_GreaterThanCondition_Test()
        {
            var compiler = new OrcaleCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var result = builder.Delete(e => e.Id > 1) as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(1, result.QueryParameters.Count);
            Assert.Equal(1, result.QueryParameters[":P0"]);
            Assert.Equal("DELETE FROM Employee WHERE (\"Id\" > :P0)", result.SqlQuery);
        }

        [Fact]
        public void Delete_GreaterThanEqualCondition_Test()
        {
            var compiler = new OrcaleCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var result = builder.Delete(e => e.Id >= 1) as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(1, result.QueryParameters.Count);
            Assert.Equal(1, result.QueryParameters[":P0"]);
            Assert.Equal("DELETE FROM Employee WHERE (\"Id\" >= :P0)", result.SqlQuery);
        }

        [Fact]
        public void Delete_LessThanCondition_Test()
        {
            var compiler = new OrcaleCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var result = builder.Delete(e => e.Id < 1) as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(1, result.QueryParameters.Count);
            Assert.Equal(1, result.QueryParameters[":P0"]);
            Assert.Equal("DELETE FROM Employee WHERE (\"Id\" < :P0)", result.SqlQuery);
        }

        [Fact]
        public void Delete_LessThanEqualCondition_Test()
        {
            var compiler = new OrcaleCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var result = builder.Delete(e => e.Id <= 1) as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(1, result.QueryParameters.Count);
            Assert.Equal(1, result.QueryParameters[":P0"]);
            Assert.Equal("DELETE FROM Employee WHERE (\"Id\" <= :P0)", result.SqlQuery);
        }

        [Fact]
        public void Delete_ContainsCondition_Test()
        {
            var compiler = new OrcaleCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var result = builder.Delete(e => e.Name.Contains("test")) as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(1, result.QueryParameters.Count);
            Assert.Equal("test", result.QueryParameters[":P0"]);
            Assert.Equal("DELETE FROM Employee WHERE (\"Name\" LIKE '%' || :P0 || '%')", result.SqlQuery);
        }

        [Fact]
        public void Delete_ContainsWithAndCondition_Test()
        {
            var compiler = new OrcaleCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var result = builder.Delete(e => e.Id == 1 && e.Name.Contains("test")) as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal(2, result.QueryParameters.Count);
            Assert.Equal(1, result.QueryParameters[":P0"]);
            Assert.Equal("test", result.QueryParameters[":P1"]);
            Assert.Equal("DELETE FROM Employee WHERE ((\"Id\" = :P0) AND (\"Name\" LIKE '%' || :P1 || '%'))", result.SqlQuery);
        }

        [Fact]
        public void Delete_WithoutCondition_Test()
        {
            var compiler = new OrcaleCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var result = builder.Delete() as DbCompileResult;

            Assert.NotNull(result);
            Assert.Equal("DELETE FROM Employee", result.SqlQuery);
        }
    }
}