using SNSRi.Repository.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Repository.Commands
{
    public abstract class BaseCommand<TEntity> : BaseRepository
    {
        public BaseCommand() : base(ConnectionFactory.CreateSQLiteConnection())
        {
        }

        public abstract int Create(TEntity entity);
        public abstract void Delete(int Id);
        public abstract void Update(TEntity entity);
    }
}
