using System.Web;
using System.Web.Optimization;

namespace Mailroom
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/angular/core")
            .Include("~/Scripts/config.js")
            .Include("~/Scripts/Blob.js")
            .Include("~/Scripts/angular-file-saver.min.js")
            .IncludeDirectory("~/Scripts/Controllers", "*.js")
            .Include("~/Scripts/core.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery")
               .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new StyleBundle("~/Common/css")
                .Include("~/Content/css/common/custom.css"));

            bundles.Add(new StyleBundle("~/Common/css")
                .Include("~/Content/css/common/custom.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs")
                .Include("~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/uibootstrapjs")
                .Include("~/Scripts/angular-ui/ui-bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/Common/js")
            .Include("~/Content/js/common/common.js"));

            bundles.Add(new StyleBundle("~/Vendor/css")
                .Include("~/Content/css/sweetalert/sweetalert.css")
                .Include("~/Content/css/select2/select2.css")
                .Include("~/Content/css/datepicker/bootstrap-datepicker.css"));
            
            bundles.Add(new ScriptBundle("~/Vendor/js")
            .Include("~/Content/js/sweetalert/sweetalert.min.js")
            .Include("~/Content/js/datepicker/bootstrap-datepicker.min.js")
            .Include("~/Content/js/datepicker/bootstrap-datepicker.es.min.js"));

            bundles.Add(new ScriptBundle("~/Vendor/js/es")
            .Include("~/Content/js/datepicker/locales/bootstrap-datepicker.es.min.js"));

            bundles.Add(new StyleBundle("~/AngularVendor/css")
                .Include("~/Content/css/select/select.css"));

            bundles.Add(new ScriptBundle("~/AngularVendor/js")
            .Include("~/Content/js/select/select.min.js"));

            bundles.Add(new ScriptBundle("~/Angular/js")
            .Include("~/Content/js/angular/angular.min.js")
            .Include("~/Content/js/angular/angular-route.min.js")
            .Include("~/Content/js/angular/angular-sanitize.min.js")
            .Include("~/Scripts/i18n/angular-locale_es-ar.js"));

            bundles.Add(new ScriptBundle("~/Pagination/js")
            .Include("~/Scripts/dirPagination.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
