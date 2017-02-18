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
        private IUnitOfWork _unitOfWork;
        private IDeviceRepository _deviceRepository;
        public DevicesController(IUnitOfWork unitOfWork, IDeviceRepository deviceRepository)
        {
            _unitOfWork = unitOfWork;
            _deviceRepository = deviceRepository;
        }

        [HttpGet]
        [Route("api/Devices")]
        public IHttpActionResult GetDevices()
        {
            return Ok(_deviceRepository.GetAll(1, 1000));
        }

        [Route("api/Rooms/{roomId}/Devices")]
        public IHttpActionResult GetDevicesByRoom(int roomId)
        {
            return Ok(_deviceRepository.GetByRoomId(roomId));
        }

        [HttpPost]
        [Route("api/CreateDevice")]
        [ResponseType(typeof(int))]
        public IHttpActionResult CreateDevice(Device device)
        {
            device.CreatedOn = DateTime.Now;
            _unitOfWork.Devices.Add(device);
            _unitOfWork.Complete();
            return Ok(device.Id);
        }

        [HttpPost]
        [Route("api/UpdateDevice")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateDevice(Device model)
        {
            var device = _unitOfWork.Devices.Get(model.Id);
            device.ModifiedOn = DateTime.Now;
            device.Name = model.Name;
            device.HideFromView = model.HideFromView;
            device.ReferenceId = model.ReferenceId;
            device.Status = model.Status;
            device.Value = model.Value;
            _unitOfWork.Complete();
            return StatusCode(HttpStatusCode.NoContent);
        }


        [HttpPost]
        [Route("api/DeleteDevice/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteDevice(int id)
        {
            _unitOfWork.Devices.Remove(id);
            _unitOfWork.Complete();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
