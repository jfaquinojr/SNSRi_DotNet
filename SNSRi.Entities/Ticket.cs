using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Entities
{
	public class Ticket
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string TicketType { get; set; }
		public string Status { get; set; }
        public string Severity { get; set; }
        public string Description { get; set; }
		public DateTime CreatedOn { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
		public int? ModifiedBy { get; set; }

		public List<Activity> Activities { get; set; } = new List<Activity>();
	}
}
