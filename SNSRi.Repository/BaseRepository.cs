using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Repository
{
    public class BaseRepository
    {
        protected IDbConnection _connection;

        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }
    }
}
