using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SNSRi.Entities;

namespace SNSRi.Repository.Query
{
	public class TicketQuery : PagedQuery<Ticket>
	{
		public IEnumerable<Ticket> GetOpenEventTickets()
		{
			//var results = Search("TicketType = 'Event' and Status = 'Open'");

			var lookup = new Dictionary<int, Ticket>();
			_connection.Query<Ticket, Activity, Ticket>(@"
				select t.*, a.* from Ticket t
				join Activity a on t.Id = a.TicketId
				where t.TicketType = 'Event' and t.Status = 'Open'
			" + generatePagingSQL(), (t, a) =>
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
			return result;
		}
	}
}
