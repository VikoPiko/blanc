using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blanc.Repositories
{
    internal abstract class RepositoryBase
    {
        private readonly string _connectionString;

        public RepositoryBase()
        {
            _connectionString = "Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01;Initial Catalog=BlankBase;Integrated Security=True;";
        }

        protected SqlConnection GetConnection() 
        {
            return new SqlConnection(_connectionString);
        }
    }
}
