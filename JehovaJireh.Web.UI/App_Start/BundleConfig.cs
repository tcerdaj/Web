using System.Web;
using System.Web.Optimization;

namespace JehovaJireh.Web.UI
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));


			bundles.Add(new ScriptBundle("~/bundles/common").Include(
					  "~/Scripts/common.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css",
					   "~/Content/knockout-file-bindings.css"));

			bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
					 "~/Scripts/knockout-{version}.debug.js",
					 "~/Scripts/knockout.validation.debug.js",
					 "~/Scripts/knockout-file-bindings.js"
					 ));

			bundles.Add(new ScriptBundle("~/bundles/maskedinput").Include(
					 "~/Scripts/jquery.maskedinput.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/areYouSure").Include(
			   "~/Scripts/jquery.are-you-sure.js"
			   ));
			
			//Account/Register
			bundles.Add(new ScriptBundle("~/bundles/account/registerviewmodel")
			   .Include(
					"~/Scripts/KOViewModel/AccountViewModel/RegisterViewModel.js",
					"~/Scripts/KOViewModel/AccountViewModel/RegisterInit.js"
			   ));

		}
	}
}
