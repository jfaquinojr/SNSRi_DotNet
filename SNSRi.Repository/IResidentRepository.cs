using SNSRi.Entities;
using System.Collections.Generic;

namespace SNSRi.Repository
{
    public interface IResidentRepository : IRepository<Resident>
    {
        IEnumerable<Resident> GetAllByRoomId(int Id);
    }
}