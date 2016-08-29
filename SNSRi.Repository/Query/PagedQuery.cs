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
	/// <summary>
	/// Pagination is patterned against the github way
	/// https://developer.github.com/v3/#pagination
	/// basically, ?page and ?per_page is expected from the request.
	/// PagedQuery provides default page values for this purpose.
	/// </summary>
	public abstract class PagedQuery<T> : BaseQuery<T>
	{
		private const int CONST_DEFAULT_PAGE = 1;
		private const int CONST_DEFAULT_PERPAGE = 30;

		
		protected int _page;
		protected int _perPage;

		public PagedQuery(IDbConnection connection) : this(connection, CONST_DEFAULT_PAGE, CONST_DEFAULT_PERPAGE)
		{
		}

		public PagedQuery(IDbConnection connection, int page, int perPage)
		{
			_connection = connection;
			_page = page;
			_perPage = perPage;

			if (_page < 1) _page = 1;
			if (_perPage < 1) _perPage = 30;	
		}

		protected override string generateBaseSQL()
		{
			return base.generateBaseSQL() + generatePagingSQL();
		}

		protected virtual string generatePagingSQL(params string[] sql)
		{
			return $" limit {_perPage} offset {(_page - 1) * _perPage}";
		}

		public override IEnumerable<T> Search(string where = "", string order = "")
		{
			var sql = base.generateBaseSQL();
			if (!string.IsNullOrEmpty(where))
			{
				sql += $" and {where} ";
			}

			if (!string.IsNullOrEmpty(order))
			{
				sql += $" order by {order} ";
			}

			sql += this.generatePagingSQL();

			return _connection.Query<T>(sql);
		}

		public override T GetById(int Id)
		{
			var sql = base.generateBaseSQL() + $" and Id = {Id}";
			var entity = _connection.QueryFirst<T>(sql);
			return entity;
		}
	}
}
