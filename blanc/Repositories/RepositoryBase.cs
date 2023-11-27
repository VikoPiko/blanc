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
            // Kogato polzvate prilojenieto mahaite bazata dokato ne si q drupnete; - Viko
            // V userRepository-to komentirate Authenticate user & getbyUsername funkciite i ne bi trqbvalo da imate problemi! ;) - Viko
            // Az kato bachkam sh si vruzvam bazata da sledq vsichko li e kakto trqbva - Viko
            // _connectionString = "Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01;Initial Catalog=BlankBase;Integrated Security=True"; - Viko DB
            //_connectionString = "Server=localhost\\MSSQLSERVER01; Database = master; Trusted_Connection = True"; - Na Emo DB
            _connectionString = "Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01;Initial Catalog=BlankBase;Integrated Security=True";
        }
        protected SqlConnection GetConnection() 
        {
            return new SqlConnection(_connectionString);
        }
    }
}
