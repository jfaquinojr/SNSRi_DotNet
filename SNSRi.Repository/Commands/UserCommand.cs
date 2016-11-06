using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SNSRi.Entities;

namespace SNSRi.Repository.Commands
{
	public class UserCommand : BaseCommand<User>
	{
	    public override int Create(User entity)
	    {
            log.Debug("Create Enter");

            entity.CreatedOn = DateTime.Now;
            const string sql = "insert into user(Username, Password, Email, CreatedOn, CreatedBy) values(@Username, @Password, @Email, @CreatedOn, @CreatedBy); SELECT last_insert_rowid()";

            log.Debug($"SQL Statement: {sql}");

            var id = _connection.Query<int>(sql, entity).Single();
            log.Debug($"Inserted record with Id: {id}");

            log.Debug("Create Exit");
            return id;
        }

	    public override void Delete(User entity)
	    {
            log.Debug("Delete Enter");

            entity.CreatedOn = DateTime.Now;
	        const string sql = "delete from user where Id = @Id";

            log.Debug($"SQL Statement: {sql}");

	        _connection.Query<int>(sql, entity);

            log.Debug("Delete Exit");
        }

	    public override void Update(User user)
	    {
            log.Debug("Update Enter");

            user.ModifiedOn = DateTime.Now;

            var sql = $@"
                update user set 
                Username = @Username,
                Password = @Password,
                Email = @Email,
                ModifiedOn = @ModifiedOn,
                ModifiedBy = 1
                where Id = @Id
            ";

            log.Debug($"SQL Statement: {sql}");

	        _connection.Query<int>(sql, user);

            log.Debug("Update Exit");

	    }
	}
}
