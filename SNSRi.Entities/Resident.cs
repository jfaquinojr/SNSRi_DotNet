using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Entities
{
	public class Resident
	{
		public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedOn { get; set; }
		public DateTime? ModifiedOn { get; set; }
		public int? CreatedBy { get; set; }
		public int? ModifiedBy { get; set; }
        public int? UIRoomId { get; set; }

        //[ForeignKey("UIRoomId")]
        public virtual UIRoom UIRoom { get; set; }
	}
}
