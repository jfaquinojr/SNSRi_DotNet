using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using SNSRi.Entities;

namespace SNSRi.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			// odata routes
			ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
			builder.EntitySet<Device>("Devices");
			builder.EntitySet<User>("Users");
			builder.EntitySet<Event>("Events");
			builder.EntitySet<UIRoom>("UIRooms");
			builder.EntitySet<UIRoomDevice>("UIRoomDevices");
			config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
		}
    }
}
