using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Entities
{
	public class Device
	{
		public int Id { get; set; }
		public int ReferenceId { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }
		public DateTime? CreatedOn { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
		public int? ModifiedBy { get; set; }
	    public string Value { get; set; }
	    public bool HideFromView { get; set; }
        public string TileGroup { get; set; }
        public int? TileSize { get; set; }
        public string TileImage { get; set; }

        public virtual ICollection<DeviceControl> DeviceControls { get; set; }
    }

    enum TileSizes
    {
        Normal = 0,
        Wide = 1,
        Small = 2,
        Large = 3,
    }
}
