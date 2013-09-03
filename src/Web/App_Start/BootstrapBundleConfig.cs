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
            var nullBuilder = new NullBuilder();
            var cssTransformer = new CssTransformer();
            var jsTransformer = new JsTransformer();
            var nullOrderer = new NullOrderer();

			// Add @Styles.Render("~/Content/bootstrap/base") in the <head/> of your _Layout.cshtml view
			// For Bootstrap theme add @Styles.Render("~/Content/bootstrap/theme") in the <head/> of your _Layout.cshtml view
			// Add @Scripts.Render("~/bundles/bootstrap") after jQuery in your _Layout.cshtml view
			// When <compilation debug="true" />, MVC4 will render the full readable version. When set to <compilation debug="false" />, the minified version will be rendered automatically
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));

            var commonStylesBundle = new Bundle("~/Content/bootstrap/base");

			commonStylesBundle
                .Include("~/Content/bootstrap/bootswatch.min.css")
                .Include("~/Content/bootstrap/bootstrap.min.css")
                .Include("~/Content/bootstrap/custom.less");

            commonStylesBundle.Builder = nullBuilder;
            commonStylesBundle.Transforms.Add(cssTransformer);
            commonStylesBundle.Orderer = nullOrderer;

            BundleTable.Bundles.Add(commonStylesBundle);

		    BundleTable.Bundles.Add(new StyleBundle("~/Content/login").Include("~/Content/login.css"));
		}
	}
}
