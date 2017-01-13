using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Entities
{
	public class EventTicket
	{
		public int Id { get; set; }
		public int EventId { get; set; }
		public int TicketId { get; set; }
	    public Event Event { get; set; }
	    public Ticket Ticket { get; set; }
	}
}
