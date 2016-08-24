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
	/// basically, ?page and ?per_page is expected from the request.
	/// PagedQuery provides default page values for this purpose.
	/// </summary>
	public abstract class PagedQuery<T> : BaseQuery<T>
	{
		private const int CONST_DEFAULT_PAGE = 1;
		private const int CONST_DEFAULT_PERPAGE = 30;

		protected IDbConnection _connection;
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
	}
}
