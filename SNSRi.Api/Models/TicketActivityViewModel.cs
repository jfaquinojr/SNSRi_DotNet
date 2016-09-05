using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNSRi.Api.Models
{
	public class TicketActivityViewModel
	{
		public int Id { get; set; }
		public int TicketId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime TicketDateTime { get; set; }

		public int ActivityId { get; set; }
		public string Comment { get; set; }
		public DateTime ActivityDateTime { get; set; }
		public string ActivityUserName { get; set; }
	}
}