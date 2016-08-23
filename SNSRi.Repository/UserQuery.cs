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
	/// <summary>
	/// Pagination is patterned against the github way
	/// https://developer.github.com/v3/#pagination
	/// basically, ?page and ?per_page is expected.
	/// otherwise, pagination defaults to 30 per page.
	/// </summary>
	public class UserQuery
	{
		const int CONST_DEFAULT_PAGE = 1;
		const int CONST_DEFAULT_PERPAGE = 30;

		IDbConnection _connection;
		int _page;
		int _perPage;

		public UserQuery() : this(CONST_DEFAULT_PAGE, CONST_DEFAULT_PERPAGE)
		{
		}

		public UserQuery(int page, int perPage)
		{
			_connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
			_page = page;
			_perPage = perPage;

			if (_page < 1) _page = 1;
			if (_perPage < 1) _perPage = 30;
		}

		public UserQuery(IDbConnection connection, int page, int perPage) : this(page, perPage)
		{
			_connection = connection;
		}

		public IEnumerable<User> Search(string whereClause="", string orderByClause="")
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

			sql += $" limit {_perPage} offset {_page * _perPage}";

			var users = _connection.Query<User>(sql);

			return users;
		}

		public User GetById(int Id)
		{
			string sql = $"select * from User where Id = {Id}";
			var users = _connection.Query<User>(sql);

			return users.FirstOrDefault();
		}
	}
}
