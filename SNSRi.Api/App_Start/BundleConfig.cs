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

			bundles.Add(new ScriptBundle("~/bundles/main").Include(
					  "~/Scripts/jquery-2.1.3.js",
					  "~/Scripts/angular.js",
					  "~/Scripts/angular-route.js",
					  "~/Scripts/angular-animate.js",
					  "~/Scripts/metro.js"));

			bundles.Add(new StyleBundle("~/Content/main").Include(
					  "~/Content/metro.css",
					  "~/Content/metro-icons.css",
					  "~/Content/metro-colors.css",
					  "~/Content/site.css"));
					  //"~/Content/metro-responsive.css",
					  //"~/Content/metro-schemes.css"));
		}
	}
}
