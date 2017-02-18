using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Repository
{
	class ConnectionFactory
	{
		internal static IDbConnection CreateSQLiteConnection()
		{
            return new SQLiteConnection(GetConnectionString());
		}

        internal static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
	}
}
