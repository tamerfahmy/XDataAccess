using System;
using Npgsql;
using XDataAccess.Dapper.Tests.TestModels;
using XDataAccess.QueryBuilder.Compilers.Databases;
using XDataAccess.QueryBuilder.Logger;
using Xunit;

namespace XDataAccess.Dapper.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var connStr = "User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=DemoForGenericRepos;Pooling=true;";

            var compiler = new PostgresCompiler();
            var logger = new ConsoleLogger(LogLevel.Debug | LogLevel.Error | LogLevel.Warning);
            using (var crud = new Crud(compiler, logger, () => { return new NpgsqlConnection(connStr); }))
            {
                try
                {
                    var affectedRows = crud.Insert(new Employee()
                    {
                        Guid = Guid.NewGuid(),
                        FirstName = "fist name",
                        LastName = "last name",
                        Active = true,
                        CreateDate = DateTime.Now,
                    });
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
