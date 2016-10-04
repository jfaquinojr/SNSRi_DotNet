using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using log4net;

namespace SNSRi.Repository
{
    public class BaseRepository
    {
		protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.Name);

		protected IDbConnection _connection;

        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public static int ExecuteSql(string sql, IDbConnection conn)
        {
            log.Debug("ExecuteSql Enter");
            log.Debug("SQL: " + sql);
            var ret = conn.Execute(sql);
            log.Debug("ExecuteSql Exit");

            return ret;
        }

        public static int ExecuteSql(string sql)
        {
            return ExecuteSql(sql, ConnectionFactory.CreateSQLiteConnection());
        }
    }
}
