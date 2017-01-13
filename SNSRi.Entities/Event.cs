using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Entities
{
	public class Event
	{
        public Event()
        {
        }

        public Event(int deviceId, string newStatus, string oldStatus, DateTime occurredOn)
        {
            DeviceId = deviceId;
            NewStatus = newStatus;
            OldStatus = oldStatus;
            OccurredOn = occurredOn;
        }

		public int Id { get; set; }
		public int DeviceId { get; set; }
		public DateTime OccurredOn { get; set; }
		public string NewStatus { get; set; }
		public string OldStatus { get; set; }
		public string Notes { get; set; }
		public DateTime? CreatedOn { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
		public int? ModifiedBy { get; set; }
	    public Device Device { get; set; }
	}
}
