using System;
using System.Web;
using System.Web.Optimization;

namespace MegProject.Web
{
    public class BundleConfig
    {

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
                throw new ArgumentNullException("ignoreList");
            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            //ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }


        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            #region Project Bundles
            bundles.Add(new ScriptBundle("~/bundles/project").Include(
                   "~/Scripts/main.js"));
            #endregion

            #region Select2 Plugin Bundles 
            bundles.Add(new StyleBundle("~/bundles/Select2Css").Include("~/Content/select2.css"));
            bundles.Add(new ScriptBundle("~/bundles/Select2Js").Include("~/Scripts/select2.js"));
            #endregion

            #region Bootstrap Validator Bundles 

            bundles.Add(new StyleBundle("~/bundles/BootstrapValidatorCss").Include("~/Content/bootstrapvalidator/css/bootstrapValidator.css"));
            bundles.Add(new ScriptBundle("~/bundles/BootstrapValidatorJs").Include("~/Content/bootstrapvalidator/js/bootstrapValidator.js"));

            #endregion

            #region
            bundles.Add(new StyleBundle("~/bundles/JqueryConfirmCss").Include("~/Content/jquery-confirm.css"));
            bundles.Add(new StyleBundle("~/bundles/JqueryConfirmJs").Include("~/Scripts/jquery-confirm.js"));
            #endregion


        }
    }
}
