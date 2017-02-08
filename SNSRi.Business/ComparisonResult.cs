using System.Collections.Generic;
using SNSRi.Entities.HomeSeer;

namespace SNSRi.Business
{
    public class ComparisonResult
    {
        internal ComparisonResult()
        {
        }

        public List<HSDevice> AddedDevices { get; set; } = new List<HSDevice>();
        public List<HSDevice> DeletedDevices { get; set; }  = new List<HSDevice>();

        public bool HasChanges => (AddedDevices.Count > 0 || DeletedDevices.Count > 0);
    }
}