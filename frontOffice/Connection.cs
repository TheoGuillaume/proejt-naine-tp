using System.Data.SqlClient;

namespace FrontOffice
{
    public class Connection
    {
        public SqlConnection GetSqlconnect()
        {
            return new SqlConnection("Server=localhost;Database=RazorPageCrud;User Id=sa;Password=#sqlServer2023;Encrypt=True;TrustServerCertificate=True");
        }
    }
}