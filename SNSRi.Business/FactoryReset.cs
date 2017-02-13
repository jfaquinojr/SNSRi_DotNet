using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SNSRi.Entities.HomeSeer;

namespace SNSRi.Business
{
    public class FactoryReset : IDisposable, IFactoryReset
    {
        private IHttpClient _httpClient;
        public FactoryReset(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public ComparisonResult CompareDevices(IEnumerable<HSDevice> devices1, IEnumerable<HSDevice> devices2)
        {
            var result = new ComparisonResult();

            var deletedDevices = devices1.Except(devices2).ToList(); 
            var addedDevices = devices2.Except(devices1).ToList(); 

            result.AddedDevices.AddRange(addedDevices);
            result.DeletedDevices.AddRange(deletedDevices);

            return result;
        }

        public IEnumerable<HSDevice> GetHSDevices(string urlHomeSeer)
        {
            var ret = new List<HSDevice>();
            urlHomeSeer = urlHomeSeer.TrimEnd('/') + "/JSON?request=getstatus";

            var jsonData = _httpClient.GetStringAsync(urlHomeSeer);

            dynamic results = JsonConvert.DeserializeObject<dynamic>(jsonData);

            var hsDevices = results.Devices;
            foreach (var hsDev in hsDevices)
            {
                ret.Add(new HSDevice
                {
                    Name = hsDev.name,
                    Status = hsDev.status,
                    Location = hsDev.location,
                    Ref = hsDev.@ref,
                    Value = hsDev.value.ToString(),
                    HideFromView = (bool) hsDev.hide_from_view,
                    Location2 = hsDev.location2,
                    DeviceTypeString = hsDev.device_type_string,
                    LastChange = DateTime.Parse(hsDev.last_change.ToString()),
                    Relationship = hsDev.relationship,
                    //DeviceType = hsDev.associated_devices,
                    DeviceImage = hsDev.device_image,
                    UserNote = hsDev.UserNote,
                    UserAccess = hsDev.UserAccess,
                    StatusImage = hsDev.status_image
                });
            }
            return ret;
        }

        public void Dispose()
        {
        }
    }
}
