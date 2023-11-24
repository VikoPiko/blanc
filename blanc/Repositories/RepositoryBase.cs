using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace blanc.Repositories
{
    internal abstract class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            // "Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01;Initial Catalog=BlankBase;Integrated Security=True;"
            _connectionString = "Server=localhost\\MSSQLSERVER01; Database = master; Trusted_Connection = True"; 
        }
        protected SqlConnection GetConnection() 
        {
            return new SqlConnection(_connectionString);
        }
    }
}
