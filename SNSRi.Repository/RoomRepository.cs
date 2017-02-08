using System.Data.Entity;
using SNSRi.Entities;

namespace SNSRi.Repository
{

    public class RoomRepository: Repository<UIRoom>, IRoomRepository
    {
        public RoomRepository(DbContext context) : base(context)
        {
        }
    }
}