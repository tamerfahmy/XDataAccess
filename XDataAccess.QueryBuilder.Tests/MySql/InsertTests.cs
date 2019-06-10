using XDataAccess.QueryBuilder.Attributes;
using XDataAccess.QueryBuilder.Compilers.Databases;
using Xunit;
using System;
using XDataAccess.QueryBuilder.Tests.TestData;

namespace XDataAccess.QueryBuilder.Tests.MySql
{
    public class InsertTests
    {
        [Fact]
        public void Insert_WithoutColumOrAttribute_Test()
        {
            var compiler = new MySqlCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            var employee = new Employee()
            {
                Id = 1,
                Name = "Employee Name"
            };

            builder.Insert(employee);

            //var sql = builder.SqlQuery;

            //Assert.NotNull(builder.QueryParameters);
            //Assert.Equal(2, builder.QueryParameters.Count);
            //Assert.Equal(builder.QueryParameters["@P0"], employee.Id);
            //Assert.Equal(builder.QueryParameters["@P1"], employee.Name);
            //Assert.Equal("INSERT INTO Employee (`Id`, `Name`) VALUES (@P0, @P1)", sql);
        }

        [Fact]
        public void Insert_WithColumOrAttribute_Test()
        {
            var compiler = new MySqlCompiler();
            var builder = new QueryBuilder<EmployeeWithAttributes>(compiler);

            var employee = new EmployeeWithAttributes()
            {
                Id = 1,
                Name = "Employee Name"
            };

            builder.Insert(employee);

            //var sql = builder.SqlQuery;

            //Assert.NotNull(builder.QueryParameters);
            //Assert.Equal(1, builder.QueryParameters.Count);
            //Assert.Equal(builder.QueryParameters["@P0"], employee.Name);
            //Assert.Equal("INSERT INTO dbo.employee (`name`) VALUES (@P0)", sql);
        }
    }
}