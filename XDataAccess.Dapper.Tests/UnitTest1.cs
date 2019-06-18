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
        private string conStr = "User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=DemoForGenericRepos;Pooling=true;";

        [Fact]
        public void Test1()
        {
            var compiler = new PostgresCompiler();
            var logger = new ConsoleLogger(LogLevel.Debug | LogLevel.Error | LogLevel.Warning);
            using (var crud = new Crud(compiler, logger, () => { return new NpgsqlConnection(conStr); }))
            {
                // Assert.Throws<System.Net.Sockets.SocketException>(() =>
                // {
                //     var affectedRows = crud.Insert(new Employee()
                //     {
                //         Guid = Guid.NewGuid(),
                //         FirstName = "fist name",
                //         LastName = "last name",
                //         Active = true,
                //         CreateDate = DateTime.Now,
                //     });
                //     Assert.Equal(1, affectedRows);
                // });
            }
        }

        [Fact]
        public void Test2()
        {
            var connStr = "User ID=postgres;Password=1234;Host=localhost;Port=5432;Database=DemoForGenericRepos;Pooling=true;";

            var compiler = new PostgresCompiler();
            var logger = new ConsoleLogger(LogLevel.Debug | LogLevel.Error | LogLevel.Warning);
            using (var crud = new Crud(compiler, logger, () => { return new NpgsqlConnection(conStr); }))
            {
                try
                {
                    var employee = crud.Select<Employee>(e => e.FirstName == "fist name");
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}