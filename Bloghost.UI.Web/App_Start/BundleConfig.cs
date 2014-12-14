using System.Web.Optimization;

namespace Bloghost.UI.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.9.1.min.js").Include
                        ("~/Scripts/functions.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.min.css",
                        "~/Content/header-1.css",
                        "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/editor/css").Include(
                        "~/Content/bootstrap/bootstrap.min.css",
                        "~/Content/bootstrap/bootstrap-theme.min.css"));

            bundles.Add(new ScriptBundle("~/Content/editor/js").Include(
                        "~/Scripts/jquery-1.9.1.min.js").Include(
                        "~/Scripts/jquery.hotkeys.js").Include(
                        "~/Scripts/bootstrap-wysiwyg.js").Include(
                        "~/Scripts/bootstrap.min.js"));
        }
    }
}