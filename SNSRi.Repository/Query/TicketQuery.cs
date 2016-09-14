using System.Collections.Generic;
using System.Linq;
using Dapper;
using log4net;
using SNSRi.Entities;

namespace SNSRi.Repository.Query
{
	public class TicketQuery : PagedQuery<Ticket>
	{

		public IEnumerable<Ticket> GetOpenEventTickets()
		{
			log.Info("GetOpenEventTickets Enter");

			var lookup = new Dictionary<int, Ticket>();
			var sql = @"
				select t.* from Ticket t
				where t.TicketType = 'Event' and t.Status = 'Open'
				order by t.CreatedOn DESC
			" + generatePagingSQL();

			log.LogSql(sql);

			var result = _connection.Query<Ticket>(sql);


			log.LogSqlResult(result.Count());
			log.Info("GetOpenEventTickets Exit");
			return result;
		}


		//public IEnumerable<Ticket> GetOpenEventTickets()
		//{
		//	log.Info("GetOpenEventTickets Enter");

		//	var lookup = new Dictionary<int, Ticket>();
		//	var sql = @"
		//		select t.* from Ticket t
		//		where t.TicketType = 'Event' and t.Status = 'Open'
		//		order by t.CreatedOn DESC
		//	" + generatePagingSQL();

		//	log.LogSql(sql);

		//	var result = _connection.Query<Ticket>(sql);


		//	log.LogSqlResult(result.Count());
		//	log.Info("GetOpenEventTickets Exit");
		//	return result;
		//}
	}
}
