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
	public class UIRoomCommand : BaseCommand<UIRoom>
	{
	    public override int Create(UIRoom entity)
	    {
            log.Debug("Create Enter");

            entity.CreatedOn = DateTime.Now;
	        const string sql = "insert into UIRoom(Name, Description, SortOrder, IsHidden, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy) values(@Name, @Description, @SortOrder, @IsHidden, @CreatedOn, @CreatedBy, @ModifiedOn, @ModifiedBy); SELECT last_insert_rowid()";
            log.Debug($"SQL Statement: {sql}");

            var id = _connection.Query<int>(sql, entity).Single();
            log.Debug($"Inserted record with Id: {id}");

            log.Debug("Create Exit");
            return id;
        }

	    public override void Delete(UIRoom entity)
	    {
	        throw new NotImplementedException();
	    }

	    public override void Update(UIRoom entity)
	    {
	        throw new NotImplementedException();
	    }
	}
}
