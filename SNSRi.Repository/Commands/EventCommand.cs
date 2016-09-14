using SNSRi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SNSRi.Repository.Query;

namespace SNSRi.Repository.Commands
{
	public class EventCommand : BaseCommand<Event>
	{
		public override int Create(Event entity)
		{
			log.Debug("Create Enter");

			entity.CreatedOn = DateTime.Now;
			const string sql =
				"insert into Event(DeviceId, OccurredOn, NewStatus, OldStatus, Notes, CreatedOn, CreatedBy) values(@DeviceId, @OccurredOn, @NewStatus, @OldStatus, @Notes, @CreatedOn, @CreatedBy); SELECT last_insert_rowid()";

			var id = _connection.Query<int>(sql, entity).Single();

			// update new status of device
			string sqlUpdateDevice = $"update Device set Status = '{entity.NewStatus}' Where Id = {entity.DeviceId}";
			_connection.Execute(sqlUpdateDevice);


			// is it actionable?
			var actionable = new Actionable(entity);
			if (actionable.IsActionable)
			{
				log.Debug($"Event with Id {id} is actionable");

				var cmdTicket = new TicketCommand();
				var ticketId = cmdTicket.Create(actionable.CreateTicket());

				string sqlCreateEventTicket = $"insert into EventTicket(EventId, TicketId) values({id}, {ticketId});";
				log.LogSql(sqlCreateEventTicket);

				_connection.Execute(sqlCreateEventTicket);
			}


			log.Debug("Create Exit");
			return id;
		}

		public override void Delete(Event entity)
		{
			throw new NotImplementedException();
		}

		public override void Update(Event entity)
		{
			throw new NotImplementedException();
		}



	}

	public class Actionable
	{
		private Event _entity;

		public Actionable(Event entity)
		{
			_entity = entity;
		}

		public bool IsActionable
		{
			get
			{
				//TODO: for now every event is actionable.
				return true;
			}
		}

		public Ticket CreateTicket()
		{
			var queryDevice = new DeviceQuery();
			var device = queryDevice.GetById(_entity.DeviceId);

			var tkt = new Ticket
			{
				Name = $"Device Status has changed",
				Description = $"Device {device.Name} status changed from {_entity.OldStatus} to {_entity.NewStatus}.",
				Status = "Open",
				TicketType = "Event"
			};

			return tkt;
		}


	}
}