using System.Collections;
using System.Collections.Generic;
using SNSRi.Entities;

namespace SNSRi.Repository
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        IEnumerable<Ticket> GetOpenEventTickets();
        IEnumerable<Ticket> GetOpenEventTicketsByRoom(int roomId);
        IEnumerable<Ticket> GetOpenTicketsPast(int ms);
        IEnumerable<Ticket> GetOpenTicketsPastMinutes(int minutes);
    }
}