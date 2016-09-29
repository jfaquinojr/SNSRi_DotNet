using SNSRi.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Repository.Query
{
	public class DeviceQuery : PagedQuery<Device>
	{
		public DeviceQuery() : base(ConnectionFactory.CreateSQLiteConnection())
		{
		}

		public DeviceQuery(int page, int perPage) : base(ConnectionFactory.CreateSQLiteConnection(), page, perPage)
		{
		}

        public Device GetByReferenceId(int referenceId)
        {
            var result = Search($"ReferenceId = {referenceId}");
            var retval = result.FirstOrDefault();
            return retval;
        }

	    public IEnumerable<Device> GetByRoomId(int roomId)
	    {
	        string sql = $@"
                select d.* from Device d
                join UIRoomDevice rd on d.Id = rd.DeviceId
                where rd.UIRoomId = {roomId}";
            var result = _connection.Query<Device>(sql).ToList();
	        return result;
	    }
	}
}
