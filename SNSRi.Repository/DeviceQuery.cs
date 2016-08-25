﻿using SNSRi.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Repository
{
	public class DeviceQuery : PagedQuery<Device>
	{
		public DeviceQuery() : base(ConnectionFactory.CreateSQLiteConnection())
		{
		}

		public DeviceQuery(int page, int perPage) : base(ConnectionFactory.CreateSQLiteConnection(), page, perPage)
		{
		}

		//protected override string generatePagingSQL(params string[] sql)
		//{
		//	var where = sql.Length > 0 ? sql[0] : "";
		//	return where + base.generatePagingSQL();
		//}
	}
}
