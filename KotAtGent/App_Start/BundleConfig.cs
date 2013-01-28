using System.Web;
using System.Web.Optimization;

namespace KotAtGent {
    public class BundleConfig {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                        "~/Scripts/site/filter.js",
                        "~/Scripts/site/home_map.js"));
            bundles.Add(new ScriptBundle("~/bundles/overzicht").Include(
                        "~/Scripts/site/details_map.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Overzicht/css").Include("~/Content/Overzicht.css", "~/Content/simplePagination.css"));
            bundles.Add(new StyleBundle("~/Home/css").Include("~/Content/Home.css"));
        }
    }
}