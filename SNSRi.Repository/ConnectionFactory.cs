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
			var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
			return new SQLiteConnection(connectionString);
		}
	}
}
