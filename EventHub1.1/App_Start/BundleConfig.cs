using System.Web;
using System.Web.Optimization;

namespace EventHub1._1
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

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                      "~/Scripts/knockout-3.1.0.js",
                      "~/Scripts/infuser.js",
                      "~/Scripts/koExternalTemplateEngine.js"));

            bundles.Add(new ScriptBundle("~/bundles/require").Include(
                      "~/Scripts/require.js"));

            bundles.Add(new ScriptBundle("~/bundles/Main").Include(
                      "~/Scripts/Main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/App")
                    .IncludeDirectory("~/Scripts/App/", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/LightBlueJS")
                    .IncludeDirectory("~/Views/LightBlue/js", "*.js"));

            bundles.Add(new StyleBundle("~/Content/LightBlueCss")
                .IncludeDirectory("~/Views/LightBlue/css", "*.css", true));

        }
    }
}