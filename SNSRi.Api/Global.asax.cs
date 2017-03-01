using OneTrueError.Client;
using Serilog;
using SNSRi.Common;
using SNSRi.Web;
using System;
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
                .WriteTo.File("log.txt")
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            var url = new Uri(Utility.GetConfig("SNSRi.OneTrueError.Url", "http://localhost:8155"));
            OneTrue.Configuration.Credentials(url, "3539919a496c422d8bd06d2c92118b25", "0596524fb94e445ca284ad9322b0b132");

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            OneTrue.Report(exception);
            Log.Error(exception, "An unhandled exception has occurred");
            Server.ClearError();
            //Response.Redirect("/Home/Error");
        }
	}
}
