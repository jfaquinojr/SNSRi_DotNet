using SNSRi.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Repository.Query
{
	public class ActivityQuery : PagedQuery<Activity>
	{
		public ActivityQuery() : base(ConnectionFactory.CreateSQLiteConnection())
		{
		}

		public ActivityQuery(int page, int perPage) : base(ConnectionFactory.CreateSQLiteConnection(), page, perPage)
		{
		}

		public IEnumerable<Activity> GetByTicketId(int ticketId)
		{
			log.Info("GetByTicketId Enter");

			var result = Search($"TicketId = {ticketId}").ToList();

			log.LogSqlResult(result.Count());
			log.Info("GetByTicketId Exit");

			return result;
		}
	}
}
