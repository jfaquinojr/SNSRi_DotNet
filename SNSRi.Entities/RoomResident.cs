using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Entities
{
	public class RoomResident
	{
		public int Id { get; set; }
        [ForeignKey("UIRoomId")]
        public int UIRoomId { get; set; }
        [ForeignKey("ResidentId")]
        public int ResidentId { get; set; }
        public DateTime? CreatedOn { get; set; }
		public DateTime? ModifiedOn { get; set; }
		public int? CreatedBy { get; set; }
		public int? ModifiedBy { get; set; }

        [NotMapped]
        public virtual UIRoom UIRoom { get; set; }
        [NotMapped]
        public virtual Resident Resident { get; set; }
    }
}
