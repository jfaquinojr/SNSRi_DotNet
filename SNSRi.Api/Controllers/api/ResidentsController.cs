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
using SNSRi.Business;

namespace SNSRi.Api.Controllers.api
{
    public class ResidentsController : ApiController
    {
        private IResidentBL _residentBL;
        public ResidentsController(IResidentBL residentBL)
        {
            _residentBL = residentBL;
        }

        [HttpGet]
        [Route("api/Residents")]
        public IHttpActionResult GetResidents()
        {
            return Ok(_residentBL.GetAll());
        }

        [HttpGet]
        [Route("api/Residents/{id}")]
        public IHttpActionResult GetResident(int id)
        {
            return Ok(_residentBL.Get(id));
        }

        [HttpPost]
        [Route("api/CreateResident")]
        [ResponseType(typeof(int))]
        public IHttpActionResult CreateResident(Resident entity)
        {
            entity.UIRoom = null;
            entity.CreatedOn = DateTime.Now;
            _residentBL.Create(entity);
            return Ok(entity.Id);
        }

        [HttpPost]
        [Route("api/UpdateResident")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateResident(Resident entity)
        {
            entity.UIRoom = null;
            entity.ModifiedOn = DateTime.Now;
            _residentBL.Update(entity);
            return StatusCode(HttpStatusCode.NoContent);
        }


        [HttpPost]
        [Route("api/DeleteResident/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteResident(int id)
        {
            _residentBL.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
