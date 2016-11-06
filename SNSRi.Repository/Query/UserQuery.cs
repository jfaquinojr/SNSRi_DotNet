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

	public class UserQuery : BaseQuery<User>
	{
		public UserQuery() : base(ConnectionFactory.CreateSQLiteConnection())
		{
		}

	    public User ValidateUser(string email, string password)
	    {
            //TODO jaquino make parametarized
	        string sql = $"select * from user where Email = '{email}' and Password = '{password}'";
	        var result = _connection.Query<User>(sql);
	        return result.FirstOrDefault();
	    }

	    public override User GetById(int Id)
	    {
	        string sql = $"select * from user where Id = {Id}";
            var result = _connection.Query<User>(sql);
            return result.FirstOrDefault();
        }
	}
}
