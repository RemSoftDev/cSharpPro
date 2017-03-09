using System.Web.Optimization;

namespace CSharpPro
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

			bundles.Add(new ScriptBundle("~/bundles/site").Include(
						"~/Scripts/jquery.validate*",
						"~/Scripts/jquery.validate.unobtrusive.js",
						"~/Scripts/jquery.unobtrusive-ajax.min.js",
						"~/Scripts/lightbox.min.js",
						"~/Scripts/jquery.isotope.min.js",
						"~/Scripts/wow.min.js",
						"~/Scripts/main.js"));

			bundles.Add(new ScriptBundle("~/bundles/adminjs").Include(
						"~/Scripts/jquery.validate*",
						"~/Scripts/jquery.validate.unobtrusive.js",
						"~/Scripts/jquery.unobtrusive-ajax.min.js",
						"~/Scripts/fastselect.js",
						"~/Scripts/jquery.ui.widget.js",
						"~/Scripts/jquery.fileupload.js",
						"~/Scripts/admin.scripts.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.min.css",
					  "~/Content/font-awesome.min.css",
					  "~/Content/animate.min.css",
					  "~/Content/lightbox.css",
					  "~/Content/main.css",
					  "~/Content/colorfull.css",
					  "~/Content/responsive.css"));

			bundles.Add(new StyleBundle("~/Content/admincss").Include(
					  "~/Content/bootstrap.min.css",
					  "~/Content/font-awesome.min.css",
					  "~/Content/responsive.css",
					  "~/Content/fastselect.css",
					  "~/Content/jquery.fileupload.css",
					  "~/Content/skin.min.css",
					  "~/Content/admin.style.css"));
		}
	}
}
