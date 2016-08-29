using SNSRi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Repository.Query
{
	public class EventQuery : PagedQuery<Event>
	{
		public EventQuery() : base(ConnectionFactory.CreateSQLiteConnection())
		{
		}

		public EventQuery(int page, int perPage) : base(ConnectionFactory.CreateSQLiteConnection(), page, perPage)
		{
		}
	}
}
