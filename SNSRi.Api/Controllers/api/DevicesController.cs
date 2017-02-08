using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SNSRi.Entities;
using SNSRi.Repository;
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
            var query = new DeviceRepository(new SNSRiContext());

            return Ok(query.GetAll(1, 1000));
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
            var uof = new UnitOfWork(new SNSRiContext());
            uof.Devices.Add(device);
            uof.Complete();
            return Ok(device.Id);
        }

        [HttpPost]
        [Route("api/UpdateDevice")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateDevice(Device model)
        {
            var uof = new UnitOfWork(new SNSRiContext());
            var device = uof.Devices.Get(model.Id);
            device.ModifiedOn = DateTime.Now;
            device.Name = model.Name;
            device.HideFromView = model.HideFromView;
            device.ReferenceId = model.ReferenceId;
            device.Status = model.Status;
            device.Value = model.Value;
            uof.Complete();
            return StatusCode(HttpStatusCode.NoContent);
        }


        [HttpPost]
        [Route("api/DeleteDevice/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteDevice(int id)
        {
            var uof = new UnitOfWork(new SNSRiContext());
            uof.Devices.Remove(id);
            uof.Complete();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
