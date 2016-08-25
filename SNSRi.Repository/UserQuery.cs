using Dapper;
using SNSRi.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Repository
{

	public class UserQuery : PagedQuery<User>
	{
		public UserQuery() : base(ConnectionFactory.CreateSQLiteConnection())
		{
		}

		public UserQuery(int page, int perPage) : base(ConnectionFactory.CreateSQLiteConnection(), page, perPage)
		{
		}

		public override IEnumerable<User> Search(string whereClause = "", string orderByClause = "")
		{
			var sql = "select * from User where 1=1";

			if (!string.IsNullOrEmpty(whereClause))
			{
				sql += $" and {whereClause}";
			}

			if (!string.IsNullOrEmpty(orderByClause))
			{
				sql += $" {orderByClause}";
			}

			sql += $" limit {_perPage} offset {(_page - 1) * _perPage}";

			var users = _connection.Query<User>(sql);

			return users;
		}

		public override User GetById(int Id)
		{
			string sql = $"select * from User where Id = {Id}";
			var users = _connection.Query<User>(sql);

			return users.FirstOrDefault();
		}

	}
}
