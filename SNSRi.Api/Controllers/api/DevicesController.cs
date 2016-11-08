using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SNSRi.Entities;
using SNSRi.Repository.Commands;
using SNSRi.Repository.Query;

namespace SNSRi.Api.Controllers.api
{
    public class DevicesController : ApiController
    {
        [HttpGet]
        [Route("api/Devices")]
        public IHttpActionResult GetDevices()
        {
            var query = new DeviceQuery();

            return Ok(query.Search());
        }

        [Route("api/Rooms/{roomId}/Devices")]
        public IHttpActionResult GetDevicesByRoom(int roomId)
        {
            var query = new DeviceQuery();

            return Ok(query.GetByRoomId(roomId));
        }

        [HttpPost]
        [Route("api/CreateDevice")]
        [ResponseType(typeof(int))]
        public IHttpActionResult CreateDevice(Device device)
        {
            device.CreatedOn = DateTime.Now;
            var cmd = new DeviceCommand();
            device.Id = cmd.Create(device);
            return Ok(device.Id);
        }

        [HttpPost]
        [Route("api/UpdateDevice")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateDevice(Device device)
        {
            device.ModifiedOn = DateTime.Now;
            var cmd = new DeviceCommand();
            cmd.Update(device);
            return StatusCode(HttpStatusCode.NoContent);
        }


        [HttpPost]
        [Route("api/DeleteDevice/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteDevice(int id)
        {
            var cmd = new DeviceCommand();
            cmd.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
