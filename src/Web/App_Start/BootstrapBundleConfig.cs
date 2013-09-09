using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Web.App_Start.BootstrapBundleConfig), "RegisterBundles")]

namespace Web.App_Start
{
    using BundleTransformer.Core.Builders;
    using BundleTransformer.Core.Orderers;
    using BundleTransformer.Core.Transformers;

    public class BootstrapBundleConfig
	{
		public static void RegisterBundles()
		{
		    var bundles = BundleTable.Bundles;

            bundles.Add(new ScriptBundle("~/Scripts/Application.js").Include(
                   "~/Scripts/jquery-1.*",
                   "~/Scripts/modernizr-*",
                   "~/Scripts/bootstrap-*",
                   "~/Scripts/jquery.unobtrusive*",
                   "~/Scripts/jquery.priceformat*"));

            bundles.Add(new StyleBundle("~/Content/Style.css").Include(
                "~/Content/bootstrap.css",
                "~/Content/fam-icons.css",
                "~/Content/datepicker.css",
                "~/Content/custom.css"));

            bundles.Add(new StyleBundle("~/Content/Theme.css").Include(
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.resizable.css",
                "~/Content/themes/base/jquery.ui.selectable.css",
                "~/Content/themes/base/jquery.ui.accordion.css",
                "~/Content/themes/base/jquery.ui.autocomplete.css",
                "~/Content/themes/base/jquery.ui.button.css",
                "~/Content/themes/base/jquery.ui.dialog.css",
                "~/Content/themes/base/jquery.ui.slider.css",
                "~/Content/themes/base/jquery.ui.tabs.css",
                "~/Content/themes/base/jquery.ui.datepicker.css",
                "~/Content/themes/base/jquery.ui.progressbar.css",
                "~/Content/themes/base/jquery.ui.theme.css"));
		}
	}
}
