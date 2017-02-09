using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Entities
{
	public class UIRoom
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int? SortOrder { get; set; }
		public bool? IsHidden { get; set; }
		public DateTime? CreatedOn { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
		public int? ModifiedBy { get; set; }
	    public string SourceRoom { get; set; }
	}
}
