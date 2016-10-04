using SNSRi.Entities;
using SNSRi.Repository.Commands;
using SNSRi.Repository.Query;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using SNSRi.Api.Models.HomeSeer;
using SNSRi.Repository;

namespace SNSRi.Api.Controllers
{
    public class CommandsController : ApiController
    {

        // POST: api/ChangeDeviceValue/5
        [HttpPost]
        [Route("api/ChangeDeviceValue/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult ChangeDeviceValue(int Id, Event entity)
        {
            var qryDevice = new DeviceQuery();
            var device = qryDevice.GetByReferenceId(Id);

            if (device == null)
            {
                var httpError = new HttpError($"Device with ReferenceId: {Id} was not found");
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, httpError);
                throw new HttpResponseException(errorResponse);
            }

            var cmdEvent = new EventCommand();
            cmdEvent.Create(new Event(device.Id, entity.NewStatus, device.Status, entity.OccurredOn));

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("api/CreateActivity")]
        [ResponseType(typeof(void))]
        public IHttpActionResult CreateActivity(Activity activity)
        {
            activity.CreatedOn = DateTime.Now;
            var cmd = new ActivityCommand();
            activity.Id = cmd.Create(activity);
            return StatusCode(HttpStatusCode.NoContent);
        }


        [HttpPost]
        [Route("api/CloseTicket")]
        [ResponseType(typeof(void))]
        public IHttpActionResult CloseTicket(Activity activity)
        {
            CreateActivity(activity);

            var cmd = new TicketCommand();
            cmd.CloseTicket(activity.TicketId);

            return StatusCode(HttpStatusCode.NoContent);
        }

        private string GetConfig(string settingName, string defaultValue)
        {
            var value = ConfigurationManager.AppSettings[settingName];
            if (string.IsNullOrWhiteSpace(value))
            {
                value = defaultValue;
            }
            return value;
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("api/FactoryReset")]
        public IHttpActionResult FactoryReset()
        {
            var url = GetConfig("HomeSeerURL", "http://localhost:8002");
            IEnumerable<HSDevice> hsDevices = GetHSDevices(url);
            HSLocation hsLocation = GetHSLocation(url);

            Truncate("UIRoomDevice");
            Truncate("UIRoom");
            Truncate("Device");

            var cmdRoom = new UIRoomCommand();
            var rooms = new List<UIRoom>();
            foreach (var hsLocationRoom in hsLocation.Rooms)
            {
                if (hsLocationRoom.ToLower() == "all") // skip this room
                    continue;

                var room = new UIRoom
                {
                    Name = hsLocationRoom,
                    CreatedOn = DateTime.Now,
                    CreatedBy = 1
                };
                room.Id = cmdRoom.Create(room);
                rooms.Add(room);
            }

            var cmdRoomDevice = new UIRoomDeviceCommand();
            var cmdDevice = new DeviceCommand();
            foreach (var hsDevice in hsDevices)
            {
                var device = new Device
                {
                    Name = hsDevice.Name,
                    ReferenceId = hsDevice.Ref,
                    Status = hsDevice.Status,
                    CreatedBy = 1,
                    CreatedOn = DateTime.Now
                };
                device.Id = cmdDevice.Create(device);

                cmdRoomDevice.Create(new UIRoomDevice
                {
                    UIRoomId = rooms.First(r => r.Name == hsDevice.Location).Id,
                    DeviceId = device.Id,
                    CreatedBy = 1,
                    CreatedOn = DateTime.Now
                });
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private IEnumerable<HSDevice> GetHSDevices(string urlHomeSeer)
        {
            var ret = new List<HSDevice>();
            urlHomeSeer = urlHomeSeer.TrimEnd('/') + "/JSON?request=getstatus";
            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(urlHomeSeer);

                dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Result);

                var hsDevices = results.Devices;
                foreach (var hsDev in hsDevices)
                {
                    ret.Add(new HSDevice
                    {
                        Name = hsDev.name,
                        Status = hsDev.value,
                        Location = hsDev.location,
                        Ref = hsDev.@ref,
                        Value = hsDev.value,
                        HideFromView = hsDev.hide_from_view,
                        Location2 = hsDev.location2
                    });
                }
            }
            return ret;
        }

        private HSLocation GetHSLocation(string urlHomeSeer)
        {
            var ret = new HSLocation();
            urlHomeSeer = urlHomeSeer.TrimEnd('/') + "/JSON?request=getlocations";
            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(urlHomeSeer);

                dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Result);

                var rooms = results.location1;
                foreach (var hsRoom in rooms)
                {
                    ret.Rooms.Add(hsRoom.ToString());
                }

                var floors = results.location2;
                foreach (var hsFloor in floors)
                {
                    ret.Floors.Add(hsFloor.ToString());
                }
            }
            return ret;
        }

        private static void Truncate(string table)
        {
            BaseRepository.ExecuteSql($"DELETE FROM {table}");
        }

    }
}
