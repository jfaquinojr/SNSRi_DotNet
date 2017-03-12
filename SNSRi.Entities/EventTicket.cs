using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
	    public Event Event { get; set; }
        [NotMapped]
	    public Ticket Ticket { get; set; }
	}
}
