using System.Data;
using System.Data.SqlClient;

namespace SmartSqlEditor.Services
{
    public class DbFactory
    {
        private static readonly string SqlConnectionString = "Server=(localdb)\\mssqllocaldb;Database=DncZeus;Trusted_Connection=True;MultipleActiveResultSets=true";
        public static IDbConnection GetDbConnection {
            get
            {
                return new SqlConnection(SqlConnectionString);
            }
        }
    }
}
