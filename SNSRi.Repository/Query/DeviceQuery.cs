using SNSRi.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Repository.Query
{
	public class DeviceQuery : PagedQuery<Device>
	{
		public DeviceQuery() : base(ConnectionFactory.CreateSQLiteConnection())
		{
		}

		public DeviceQuery(int page, int perPage) : base(ConnectionFactory.CreateSQLiteConnection(), page, perPage)
		{
		}
	}
}
