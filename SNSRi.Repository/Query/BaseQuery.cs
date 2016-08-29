using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SNSRi.Repository.Query
{
	public abstract class BaseQuery<T> : BaseRepository
	{
        public BaseQuery(IDbConnection connection) : base(connection)
        {

        }

		public abstract T GetById(int Id);

		public virtual IEnumerable<T> Search(string where = "", string order = "")
		{
			var sql = generateBaseSQL();
			if (!string.IsNullOrEmpty(where))
			{
				sql += $" and {where} ";
			}

			if (!string.IsNullOrEmpty(order))
			{
				sql += $" order by {order} ";
			}

			return _connection.Query<T>(sql);
		}

		protected virtual string generateBaseSQL()
		{
			Type type = typeof(T);
			return $"select * from {type.Name} where 1=1";
		}
	}
}
