using System.Web;
using System.Web.Optimization;

namespace SNSRi.Api
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/main").Include(
                      "~/Scripts/jquery-2.1.3.min.js",
                      "~/Scripts/metro.js",
                      "~/Scripts/angular.js",
                      "~/Scripts/xxxxxxxxxxxxxxxxxx.js",
                      "~/Scripts/xxxxxxxxxxxxxxxxxx.js",
                      "~/Scripts/xxxxxxxxxxxxxxxxxx.js",
                      "~/Scripts/xxxxxxxxxxxxxxxxxx.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/xxxxxxxxxxxxxx.css",
                      "~/Content/xxxxxxxxxxxxxx.css",
                      "~/Content/xxxxxxxxxxxxxx.css",
                      "~/Content/xxxxxxxxxxxxxx.css",
                      "~/Content/xxxxxxxxxxxxxx.css"));
        }
	}
}
