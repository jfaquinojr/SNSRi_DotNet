using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using SNSRi.Web;
using SNSRi.Web.App_Start;

namespace SNSRi.Api
{
	public class WebApiApplication : System.Web.HttpApplication
	{
        private static readonly ILog Log = LogManager.GetLogger(typeof(WebApiApplication));

        protected void Application_Start()
		{
            Log.Debug("Application started");

            AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();

            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("json", "true", "application/json"));
        }
	}
}
