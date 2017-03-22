using HibernatingRhinos.Profiler.Appender.EntityFramework;
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
                .WriteTo.RollingFile(Utility.GetConfig("SNSRi.Log.File", "C:\\temp\\logs\\log.txt"))
                .WriteTo.Seq(Utility.GetConfig("SNSRi.Seq.Url", "http://localhost:5341"))
                .CreateLogger();

            var url = new Uri(Utility.GetConfig("SNSRi.OneTrueError.Url", "http://jfaerrors.azurewebsites.net"));
            var appKey = Utility.GetConfig("SNSRi.OneTrueError.Key", "05f16bba5b5f4b41bafc7a7a649eec17");
            var secret = Utility.GetConfig("SNSRi.OneTrueError.Secret", "f0a231445ebd41aea5cb51e95d0f6303");
            OneTrue.Configuration.Credentials(url, appKey, secret);

            EntityFrameworkProfiler.Initialize(EntityFrameworkVersion.EntityFramework6);


        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            try
            {
                Log.Error(exception, "An unhandled exception has occurred {@exception}");
                OneTrue.Report(exception);
            }
            catch { }

            Server.ClearError();
            //Response.Redirect("/Home/Error");
        }
	}
}
