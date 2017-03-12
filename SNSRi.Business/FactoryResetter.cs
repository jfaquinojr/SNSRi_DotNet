using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SNSRi.Entities.HomeSeer;
using SNSRi.Repository;
using SNSRi.Common;

namespace SNSRi.Business
{
    public class FactoryResetter : IDisposable, IFactoryResetter
    {
        private IHomeSeerUnitOfWork _uof;
        private IHttpClient _httpClient;
        private readonly string _url;
        public FactoryResetter(IHttpClient httpClient, IHomeSeerUnitOfWork uof)
        {
            _httpClient = httpClient;
            _uof = uof;
            _url = Utility.GetConfig("HomeSeerURL", "http://localhost:8002");
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

            var resultsStatus = GetDevicesStatus();
            var resultsControls = GetDeviceControls();

            var hsDevices = resultsStatus.Devices;
            foreach (var hsDev in hsDevices)
            {
                var dev = new HSDevice
                {
                    Name = hsDev.name,
                    Status = hsDev.status,
                    Location = hsDev.location,
                    Ref = hsDev.@ref,
                    Value = hsDev.value.ToString(),
                    HideFromView = (bool)hsDev.hide_from_view,
                    Location2 = hsDev.location2,
                    DeviceTypeString = hsDev.device_type_string,
                    LastChange = DateTime.Parse(hsDev.last_change.ToString()),
                    Relationship = hsDev.relationship,
                    //DeviceType = hsDev.associated_devices,
                    DeviceImage = hsDev.device_image,
                    UserNote = hsDev.UserNote,
                    UserAccess = hsDev.UserAccess,
                    StatusImage = hsDev.status_image,
                    ControlPairs = resultsControls.Where(d => d.Ref == int.Parse(hsDev.@ref.ToString())).ToList()
                };

                ret.Add(dev);
            }
            return ret;

            dynamic GetDevicesStatus()
            {
                var url = urlHomeSeer.TrimEnd('/') + "/JSON?request=getstatus";
                var jsonData = _httpClient.GetStringAsync(url);
                dynamic results = JsonConvert.DeserializeObject<dynamic>(jsonData);
                return results;
            }

            List<ControlPair> GetDeviceControls()
            {
                var url = urlHomeSeer.TrimEnd('/') + "/JSON?request=getcontrol";
                var jsonData = _httpClient.GetStringAsync(url);
                dynamic results = JsonConvert.DeserializeObject<dynamic>(jsonData);

                var pairs = new List<ControlPair>();
                foreach (var device in results.Devices)
                {
                    foreach(var pair in device.ControlPairs)
                    {
                        pairs.Add(new ControlPair
                        {
                            Do_Update = pair.Do_Update,
                            SingleRangeEntry = pair.SingleRangeEntry,
                            ControlButtonType = pair.ControlButtonType,
                            ControlButtonCustom = pair.ControlButtonCustom,
                            CCIndex = pair.CCIndex,
                            Range = pair.Range,
                            Ref = pair.Ref,
                            Label = pair.Label,
                            ControlType = pair.ControlType,
                            ControlUse = pair.ControlUse,
                            ControlValue = pair.ControlValue,
                            ControlString = pair.ControlString,
                            ControlStringList = pair.ControlStringList,
                            ControlStringSelected = pair.ControlStringSelected,
                            ControlFlag = pair.ControlFlag
                        });
                    }
                }
                return pairs;
            }
        }



        public void Dispose()
        {
        }

        public void FactoryReset()
        {
            _uof.FactoryReset(GetHSDevices(_url), ObjectConverter.ConvertToDevice, ObjectConverter.ConvertToRoom);
        }

        public void FactorySync()
        {
            var currentDevices = _uof.HSDevices.GetAll();
            var hsDevices = GetHSDevices(_url);
            var result = CompareDevices(currentDevices, hsDevices);
            if (result.HasChanges)
            {
                _uof.FactorySync(result.AddedDevices, result.DeletedDevices, ObjectConverter.ConvertToDevice);
            }
        }

    }
}
