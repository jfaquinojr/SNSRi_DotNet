using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SNSRi.Entities;

namespace SNSRi.Repository.Commands
{
	public class UIRoomDeviceCommand : BaseCommand<UIRoomDevice>
	{

	    public override int Create(UIRoomDevice entity)
	    {
            log.Debug("Create Enter");

            entity.CreatedOn = DateTime.Now;
            const string sql = "insert into UIRoomDevice(UIRoomId, DeviceId, SortOrder, DisplayText, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy) values(@UIRoomId, @DeviceId, @SortOrder, @DisplayText, @CreatedOn, @CreatedBy, @ModifiedOn, @ModifiedBy); SELECT last_insert_rowid()";
            log.Debug($"SQL Statement: {sql}");

            var id = _connection.Query<int>(sql, entity).Single();
            log.Debug($"Inserted record with Id: {id}");

            log.Debug("Create Exit");
            return id;
        }

	    public override void Delete(int Id)
	    {
            log.Debug("Delete Enter");

            var sql = $"delete from UIRoomDevice where Id = {Id}";

            log.Debug($"SQL Statement: {sql}");

            _connection.Query<int>(sql);

            log.Debug("Delete Exit");
        }

	    public override void Update(UIRoomDevice entity)
	    {
	        throw new NotImplementedException();
	    }
	}
}
