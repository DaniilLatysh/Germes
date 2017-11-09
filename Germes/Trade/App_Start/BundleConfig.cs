using System.Web;
using System.Web.Optimization;

namespace Trade
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/modernizr-*",
                "~/Scripts/respond.js",
                "~/Scripts/Scripts.js",
                "~/Scripts/moment.min.js",
                "~/Scripts/bootstrap-datepicker.min.js",
                "~/Scripts/bootstrap-datetimepicker.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-*",
                "~/Content/style.css"));

        }
    }
}
