using SNSRi.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Business
{
    public interface IRoomsBL : IBusinessLayer
    {
        IEnumerable<UIRoom> GetAllRooms();
    }
}
