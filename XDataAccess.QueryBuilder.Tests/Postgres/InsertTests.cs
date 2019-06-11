using XDataAccess.QueryBuilder.Attributes;
using XDataAccess.QueryBuilder.Compilers.Databases;
using Xunit;
using System;
using XDataAccess.QueryBuilder.Tests.TestData;

namespace XDataAccess.QueryBuilder.Tests.Postgres
{
    public class InsertTests
    {
        [Fact]
        public void Insert_WithoutColumOrAttribute_Test()
        {
            var compiler = new PostgresCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var employee = new Employee()
            {
                Id = 1,
                Name = "Employee Name"
            };

            var result = builder.Insert(employee) as DbCompileResult;

            Assert.NotNull(result.QueryParameters);
            Assert.Equal(2, result.QueryParameters.Count);
            Assert.Equal(result.QueryParameters["@P0"], employee.Id);
            Assert.Equal(result.QueryParameters["@P1"], employee.Name);
            Assert.Equal("INSERT INTO Employee (\"Id\",\"Name\") VALUES (@P0,@P1)", result.SqlQuery);
        }

        [Fact]
        public void Insert_WithColumOrAttribute_Test()
        {
            var compiler = new PostgresCompiler();
            var builder = new QueryBuilder<EmployeeWithAttributes>(compiler);

            var employee = new EmployeeWithAttributes()
            {
                Id = 1,
                Name = "Employee Name"
            };

            var result = builder.Insert(employee) as DbCompileResult;

            Assert.NotNull(result.QueryParameters);
            Assert.Equal(1, result.QueryParameters.Count);
            Assert.Equal(result.QueryParameters["@P0"], employee.Name);
            Assert.Equal("INSERT INTO dbo.employee (\"name\") VALUES (@P0)", result.SqlQuery);
        }
    }
}