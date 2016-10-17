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

namespace SNSRi.Repository.Query
{

	public class UserQuery : PagedQuery<User>
	{
		public UserQuery() : base(ConnectionFactory.CreateSQLiteConnection())
		{
		}

		public UserQuery(int page, int perPage) : base(ConnectionFactory.CreateSQLiteConnection(), page, perPage)
		{
		}

	    public User ValidateUser(string email, string password)
	    {
            //TODO jaquino make parametarized
	        string sql = $"select * from user where Email = '{email}' and Password = '{password}'";
	        var result = _connection.Query<User>(sql);
	        return result.FirstOrDefault();
	    }

	}
}
