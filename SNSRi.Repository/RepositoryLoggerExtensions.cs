using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace SNSRi.Repository
{
	public static class RepositoryLoggerExtensions
	{
		public static void LogSql(this ILog log, string sqlStatement)
		{
			log.Debug($"SQL Statement: '{sqlStatement}'");
		}

		public static void LogSqlResult(this ILog log, int number)
		{
			log.Debug($"SQL Returned: '{number}'");
		}
	}
}
