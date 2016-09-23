using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SNSRi.Entities;

namespace SNSRi.Repository.Commands
{
	public class ActivityCommand : BaseCommand<Activity>
	{
		public override int Create(Activity entity)
		{
			log.Debug("Create Enter");

			entity.CreatedOn = DateTime.Now;
            const string sql = "insert into Activity(TicketId, Comment, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy) values(@TicketId, @Comment, @CreatedOn, @CreatedBy, @ModifiedOn, @ModifiedBy); SELECT last_insert_rowid()";

            log.Debug($"SQL Statement: {sql}");

			var id = _connection.Query<int>(sql, entity).Single();
			log.Debug($"Inserted record with Id: {id}");

			log.Debug("Create Exit");
			return id;
		}

		public override void Delete(Activity entity)
		{
			throw new NotImplementedException();
		}

		public override void Update(Activity entity)
		{
			throw new NotImplementedException();
		}


	}
}
