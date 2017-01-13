using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SNSRi.Entities;

namespace SNSRi.Repository.Commands
{
	public class DeviceCommand_Old : BaseCommand<Device>
	{
	    public override int Create(Device entity)
	    {
            log.Debug("Create Enter");

            entity.CreatedOn = DateTime.Now;
	        const string sql = "insert into Device(ReferenceId, Name, Status, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy, Value, HideFromView) values(@ReferenceId, @Name, @Status, @CreatedOn, @CreatedBy, @ModifiedOn, @ModifiedBy, @Value, @HideFromView); SELECT last_insert_rowid()";
            log.Debug($"SQL Statement: {sql}");

            var id = _connection.Query<int>(sql, entity).Single();
            log.Debug($"Inserted record with Id: {id}");

            log.Debug("Create Exit");
            return id;
        }

	    public override void Delete(int Id)
	    {
            log.Debug("Delete Enter");

            var sql = $"delete from Device where Id = {Id}";

            log.Debug($"SQL Statement: {sql}");

            _connection.Query<int>(sql);

            log.Debug("Delete Exit");
        }

	    public override void Update(Device entity)
	    {
            log.Debug("Update Enter");

            entity.ModifiedOn = DateTime.Now;

            var sql = $@"
                update Device set
                Id = @Id,
                ReferenceId = @ReferenceId,
                Name = @Name,
                Status = @Status,
                CreatedOn = @CreatedOn,
                CreatedBy = @CreatedBy,
                ModifiedOn = @ModifiedOn,
                ModifiedBy = @ModifiedBy,
                Value = @Value,
                HideFromView = @HideFromView
                where Id = @Id
            ";

            log.Debug($"SQL Statement: {sql}");

            _connection.Query<int>(sql, entity);

            log.Debug("Update Exit");
        }
	}
}
