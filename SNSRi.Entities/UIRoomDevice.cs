using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Entities
{
	public class UIRoomDevice
	{
		public int Id { get; set; }
		public int UIRoomId { get; set; }
		public int DeviceId { get; set; }
		public int? SortOrder { get; set; }
		public string DisplayText { get; set; }
		public DateTime? CreatedOn { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
		public int? ModifiedBy { get; set; }

	    public Device Device { get; set; }
	    public UIRoom Room { get; set; }
	}
}
