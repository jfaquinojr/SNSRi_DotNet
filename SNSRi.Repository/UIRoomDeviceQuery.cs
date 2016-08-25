using SNSRi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Repository
{
	public class UIRoomDeviceQuery : PagedQuery<UIRoomDevice>
	{
		public UIRoomDeviceQuery() : base (ConnectionFactory.CreateSQLiteConnection())
		{
		}

		public UIRoomDeviceQuery(int page, int perPage) : base (ConnectionFactory.CreateSQLiteConnection(), page, perPage)
		{
		}
	}
}
