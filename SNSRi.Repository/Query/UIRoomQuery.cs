using SNSRi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Repository.Query
{
	public class UIRoomQuery : PagedQuery<UIRoom>
	{
		public UIRoomQuery() : base(ConnectionFactory.CreateSQLiteConnection())
		{
		}

		public UIRoomQuery(int page, int perPage) : base(ConnectionFactory.CreateSQLiteConnection(), page, perPage)
		{
		}
	}
}
