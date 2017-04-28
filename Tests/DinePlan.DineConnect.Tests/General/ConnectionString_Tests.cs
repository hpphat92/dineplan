using System.Data.SqlClient;
using Shouldly;
using Xunit;

namespace DinePlan.DineConnect.Tests.General
{
    public class ConnectionString_Tests
    {
        [Fact]
        public void SqlConnectionStringBuilder_Test()
        {
            var csb = new SqlConnectionStringBuilder("Server=localhost; Database=DineConnect; Trusted_Connection=True;");
            csb["Database"].ShouldBe("DineConnect");
        }
    }
}
