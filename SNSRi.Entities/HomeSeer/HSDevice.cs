using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SNSRi.Entities.HomeSeer
{
    public class HSDevice : IEquatable<HSDevice>
    {
        public int Id { get; set; }
        public int Ref { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Location2 { get; set; }
        public string Value { get; set; }
        public string Status { get; set; }
        public bool? HideFromView { get; set; }
        public string DeviceTypeString { get; set; }
        public DateTime? LastChange { get; set; }
        public int? Relationship { get; set; }
        public string DeviceType { get; set; }
        public string DeviceImage { get; set; }
        public string UserNote { get; set; }
        public string UserAccess { get; set; }
        public string StatusImage{ get; set; }

        //public IEnumerable<string> AssociatedDevices { get; set; }
        //public List<ControlPair> ControlPairs { get; set; }



        #region IEquatable
        public override bool Equals(object obj)
        {
            return this.Equals(obj as HSDevice);
        }

        public bool Equals(HSDevice other)
        {
            if (other == null)
                return false;

            return this.Ref.Equals(other.Ref) &&
                   (
                       object.ReferenceEquals(this.Location, other.Location) ||
                       this.Location != null &&
                       this.Location.Equals(other.Location)
                   ) &&
                   (
                       object.ReferenceEquals(this.Location2, other.Location2) ||
                       this.Location2 != null &&
                       this.Location2.Equals(other.Location2)
                   )&&
                   (
                       object.ReferenceEquals(this.Name, other.Name) ||
                       this.Name != null &&
                       this.Name.Equals(other.Name)
                   );
        }

        public override int GetHashCode()
        {
            // http://stackoverflow.com/a/263416/578334
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + Ref.GetHashCode();
                hash = hash * 23 + Location.GetHashCode();
                hash = hash * 23 + Location2.GetHashCode();
                hash = hash * 23 + Name.GetHashCode();
                return hash;
            }
        }
        #endregion

    }
}