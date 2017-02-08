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
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SNSRi.Entities.HomeSeer;
using SNSRi.Repository;

namespace SNSRi.Api.Controllers
{
    //[Authorize]
    public class CommandsController : ApiController
    {

        // POST: api/ChangeDeviceValue/5
        [HttpPost]
        [Route("api/ChangeDeviceValue/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult ChangeDeviceValue(int Id, Event entity)
        {
            var qryDevice = new DeviceRepository(new SNSRiContext());
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
            return Ok(activity.Id);
        }

        [HttpPost]
        [Route("api/CreateUser")]
        [ResponseType(typeof(int))]
        public IHttpActionResult CreateUser(User user)
        {
            user.CreatedOn = DateTime.Now;
            var cmd = new UserCommand();
            user.Id = cmd.Create(user);
            return Ok(user.Id);
        }

        [HttpPost]
        [Route("api/UpdateUser")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateUser(User user)
        {
            user.ModifiedOn = DateTime.Now;
            var cmd = new UserCommand();
            cmd.Update(user);
            return StatusCode(HttpStatusCode.NoContent);
        }


        [HttpPost]
        [Route("api/DeleteUser/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteUser(int id)
        {
            var cmd = new UserCommand();
            cmd.Delete(id);
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

        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("api/FactoryReset")]
        public IHttpActionResult FactoryReset()
        {
            var uof = new HomeSeerUnitOfWork(new SNSRiContext());
            var url = Utility.GetConfig("HomeSeerURL", "http://localhost:8002");
            uof.FactoryReset(GetHSDevices(url));

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("api/FactorySync")]
        public IHttpActionResult FactorySync()
        {
            var uof = new HomeSeerUnitOfWork(new SNSRiContext());
            var url = Utility.GetConfig("HomeSeerURL", "http://localhost:8002");
            uof.FactorySync(GetHSDevices(url));

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
            }
            return ret;
        }

        private HSLocations GetHSLocation(string urlHomeSeer)
        {
            var ret = new HSLocations();
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
