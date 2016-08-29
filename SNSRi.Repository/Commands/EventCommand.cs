using SNSRi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SNSRi.Repository.Commands
{
    public class EventCommand : BaseCommand<Event>
    {
        public override int Create(Event entity)
        {
            entity.CreatedOn = DateTime.Now;
            const string sql = "insert into Event(DeviceId, OccurredOn, NewStatus, OldStatus, Notes, CreatedOn, CreatedBy) values(@DeviceId, @OccurredOn, @NewStatus, @OldStatus, @Notes, @CreatedOn, @CreatedBy); SELECT last_insert_rowid()";
            var id = _connection.Query<int>(sql, entity).Single();
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
}
