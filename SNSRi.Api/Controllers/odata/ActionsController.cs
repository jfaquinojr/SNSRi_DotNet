using System;
using System.Collections;
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

namespace SNSRi.odata.Controllers
{ 
	public class ActionsController : CustomODataController
    {

        // GET: odata/Actions
        public IHttpActionResult GetActions(ODataQueryOptions<TicketActivityViewModel> queryOptions)
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
	        var tickets = query.GetOpenEventTickets();
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

        // GET: odata/Actions(5)
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
    }
}
