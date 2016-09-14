using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace SNSRi.Repository
{
    public class BaseRepository
    {
		protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.Name);

		protected IDbConnection _connection;

        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }
    }
}
