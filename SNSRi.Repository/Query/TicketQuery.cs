using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using log4net;
using SNSRi.Entities;

namespace SNSRi.Repository.Query
{
	public class TicketQuery : PagedQuery<Ticket>
	{

		public IEnumerable<Ticket> GetOpenEventTickets()
		{
			log.Info("GetOpenEventTickets Enter");

			var sql = @"
				select t.* from Ticket t
				where t.TicketType = 'Event' and t.Status = 'Open'
				order by t.CreatedOn DESC
			" + generatePagingSQL();

			log.LogSql(sql);

			var result = _connection.Query<Ticket>(sql);


			log.LogSqlResult(result.Count());
			log.Info("GetOpenEventTickets Exit");
			return result;
		}


		public IEnumerable<Ticket> GetOpenEventTicketsByRoom(int roomId)
		{
			log.Info("GetOpenEventTicketsByRoom Enter");

			var sql = $@"
				select distinct t.*
				from Ticket t 
				join EventTicket et on t.Id = et.TicketId 
				join Event e on et.EventId = e.Id
				join Device d on e.DeviceId = d.Id
				join UIRoomDevice rd on d.Id = rd.DeviceId
				where rd.UIRoomId = {roomId}
				and t.Status = 'Open' and t.TicketType = 'Event' 
				order by t.CreatedOn DESC
			" + generatePagingSQL();

			log.LogSql(sql);

			var result = _connection.Query<Ticket>(sql);


			log.LogSqlResult(result.Count());
			log.Info("GetOpenEventTicketsByRoom Exit");
			return result;
		}

		public IEnumerable<Ticket> GetOpenTicketsPastMinutes(int minutes)
		{
			log.Info("GetOpenTicketsPastMinutes Enter");

			var pastDateTime = DateTime.Now.AddMinutes(-minutes);
			
			var sql = $@"
				select t.* from Ticket t
				where t.TicketType = 'Event' and t.Status = 'Open'
				and t.CreatedOn >= DateTime('{pastDateTime.ToString("yyyy-MM-dd HH:mm:ss")}')
				order by t.CreatedOn DESC
			" + generatePagingSQL();

			log.LogSql(sql);

			var result = _connection.Query<Ticket>(sql);


			log.LogSqlResult(result.Count());
			log.Info("GetOpenTicketsPastMinutes Exit");
			return result;
		}
	}
}
