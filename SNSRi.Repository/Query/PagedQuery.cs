using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

		public PagedQuery() : this(ConnectionFactory.CreateSQLiteConnection())
		{
		}

		public PagedQuery(IDbConnection connection) : this(connection, CONST_DEFAULT_PAGE, CONST_DEFAULT_PERPAGE)
		{
		}

		public PagedQuery(IDbConnection connection, int page, int perPage) : base(connection)
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

		public override IEnumerable<T>Search(string where = "", string order = "")
        {
            log.Info("Search Enter");
            
            var sql = base.generateBaseSQL();

            sql = appendWhereAndSortClause(sql, where, order);

            sql += this.generatePagingSQL();

            log.Debug($"SQL generated: '{sql}'");

            var result = _connection.Query<T>(sql).ToList();

            log.Debug($"Dapper return {result.Count()} record(s)");
            log.Info("Search Exit");

            return result;
        }

        protected virtual string appendWhereAndSortClause(string sql, string where, string order)
        {
            if (!string.IsNullOrEmpty(where))
            {
                sql += $" and {where} ";
            }

            if (!string.IsNullOrEmpty(order))
            {
                sql += $" order by {order} ";
            }

            return sql;
        }

        public override T GetById(int Id)
		{
            log.Info("GetById Enter");

			var sql = base.generateBaseSQL() + $" and Id = {Id}";
			var entity = _connection.QueryFirst<T>(sql);

            log.Debug($"SQL generated is {sql}");
            log.Debug($"Result is {(entity == null ? "Found" : "Empty")}");
            log.Info("GetById Exit");

			return entity;
		}
	}
}
