using XDataAccess.QueryBuilder.Compilers.Databases;
using XDataAccess.QueryBuilder.Tests.TestData;
using Xunit;

namespace XDataAccess.QueryBuilder.Tests.SqlServer
{
    public class DeleteTests
    {
        [Fact]
        public void Insert_WithoutColumOrAttribute_Test()
        {
            var compiler = new SqlServerCompiler();
            var builder = new QueryBuilder<Employee>(compiler);

            builder.Delete(e => e.Id == 1);
        }
    }
}