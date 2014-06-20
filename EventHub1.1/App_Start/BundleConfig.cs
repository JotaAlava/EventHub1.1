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

            bundles.Add(new ScriptBundle("~/bundles/blockui").Include(
                      "~/Scripts/blockui.js"));

            bundles.Add(new ScriptBundle("~/bundles/d3").Include(
                      "~/Scripts/d3.js"));

            bundles.Add(new ScriptBundle("~/bundles/timepicker").Include(
                      "~/Scripts/bootstrap-timepicker.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/EventHub.css",
                      "~/Content/bootstrap-timepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/App")
                    .IncludeDirectory("~/Scripts/App/", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                      "~/Scripts/select2.js"));

            bundles.Add(new ScriptBundle("~/bundles/validate").Include(
                      "~/Scripts/validate.js"));

            bundles.Add(new ScriptBundle("~/bundles/underscore").Include(
                      "~/Scripts/underscore.js"));

            bundles.Add(new StyleBundle("~/Content/select2").Include(
                      "~/Content/select2.css"));
        }
    }
}