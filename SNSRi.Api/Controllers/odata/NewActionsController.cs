using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using SNSRi.Api.Models;
using Microsoft.Data.OData;
using SNSRi.odata.Controllers;
using SNSRi.Repository.Query;
using WebGrease.Css.Extensions;

namespace SNSRi.Api.Controllers.odata
{

    public class NewActionsController : CustomODataController
    {

        // GET: odata/NewActions
        public IHttpActionResult GetNewActions(ODataQueryOptions<TicketActivityViewModel> queryOptions)
        {
            // validate the query.
            try
            {
				_validationSettings = new ODataValidationSettings
				{
					AllowedFunctions = AllowedFunctions.None,
					AllowedLogicalOperators = AllowedLogicalOperators.None,
					AllowedArithmeticOperators = AllowedArithmeticOperators.None,
					AllowedQueryOptions = AllowedQueryOptions.None
				};
				queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

			var query = new TicketQuery();
			var tickets = query.GetLatestEventTickets();
			var tktActivities = new List<TicketActivityViewModel>();
			var displayId = 0;
			tickets.ForEach(t =>
			{
				t.Activities.ForEach(a =>
				{
					tktActivities.Add(new TicketActivityViewModel
					{
						Id = ++displayId,
						ActivityDateTime = a.CreatedOn,
						ActivityId = a.Id,
						ActivityUserName = "somebody",
						Comment = a.Comment,
						Description = t.Description,
						Name = t.Name,
						TicketDateTime = t.CreatedOn,
						TicketId = t.Id
					});
				});
			});
			
			return Ok<IEnumerable<TicketActivityViewModel>>(tktActivities);
		}

        // GET: odata/NewActions(5)
        public IHttpActionResult GetTicketActivityViewModel([FromODataUri] int key, ODataQueryOptions<TicketActivityViewModel> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            // return Ok<TicketActivityViewModel>(ticketActivityViewModel);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: odata/NewActions(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<TicketActivityViewModel> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(ticketActivityViewModel);

            // TODO: Save the patched entity.

            // return Updated(ticketActivityViewModel);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/NewActions
        public IHttpActionResult Post(TicketActivityViewModel ticketActivityViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(ticketActivityViewModel);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/NewActions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<TicketActivityViewModel> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(ticketActivityViewModel);

            // TODO: Save the patched entity.

            // return Updated(ticketActivityViewModel);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/NewActions(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
