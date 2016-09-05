using System.Collections.Generic;
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
				select t.*, a.* from Ticket t
				join Activity a on t.Id = a.TicketId
				where t.TicketType = 'Event' and t.Status = 'Open'
				order by t.CreatedOn DESC, a.CreatedOn DESC
			" + generatePagingSQL();
			log.Debug($"SQL statement: '{sql}'");
			_connection.Query<Ticket, Activity, Ticket>(sql, (t, a) =>
			{
				Ticket tkt;
				if (!lookup.TryGetValue(t.Id, out tkt))
				{
					lookup.Add(t.Id, tkt = t);
				}
				tkt.Activities.Add(a);
				return tkt;
			});

			var result = lookup.Values;
			log.Debug($"SQL Returned {result.Count} rows.");
			log.Info("GetOpenEventTickets Exit");
			return result;
		}
	}
}
