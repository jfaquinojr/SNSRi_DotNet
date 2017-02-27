using Serilog;
using SNSRi.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SNSRi.Api
{
    public class WebApiApplication : System.Web.HttpApplication
	{
        protected void Application_Start()
		{
            Log.Debug("Application started");

            AreaRegistration.RegisterAllAreas();

            RouteTable.Routes.MapHubs();

            GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.ConfigureMapping();

            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("json", "true", "application/json"));
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Stackify()
                .CreateLogger();
        }
	}
}
