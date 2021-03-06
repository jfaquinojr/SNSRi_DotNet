﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SNSRi.Entities;

namespace SNSRi.Repository.Commands
{
	public class TicketCommand : BaseCommand<Ticket>
	{
		public override int Create(Ticket entity)
		{
			log.Debug("Create Enter");

			entity.CreatedOn = DateTime.Now;
			const string sql = "insert into Ticket(Name, TicketType, Status, Description, CreatedOn, CreatedBy) values(@Name, @TicketType, @Status, @Description, @CreatedOn, @CreatedBy); SELECT last_insert_rowid()";
			log.Debug($"SQL Statement: {sql}");

			var id = _connection.Query<int>(sql, entity).Single();
			log.Debug($"Inserted record with Id: {id}");

			log.Debug("Create Exit");
			return id;
		}

	    public void CloseTicket(int ticketId)
	    {
	        
            log.Debug("CloseTicket Enter");

            const string sql = "update Ticket set Status = 'Closed', ModifiedOn = DateTime('now') where Id = @Id";
            log.Debug($"SQL Statement: {sql}");

	        _connection.Execute(sql, new {Id = ticketId});
  
            log.Debug("CloseTicket Exit");
        }

		public override void Delete(int Id)
		{
			throw new NotImplementedException();
		}

		public override void Update(Ticket entity)
		{
			throw new NotImplementedException();
		}


	}
}
