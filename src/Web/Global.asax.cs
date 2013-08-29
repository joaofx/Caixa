namespace Web
{
    using App_Start;
    using Felice.Core;
    using Felice.Core.IoC;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Felice.Data;
    using Felice.Mvc;
    using Infra.Migrations;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            FeliceCore.Initialize();

            Database.Initialize();
            ////Database.UpdateSchema();
            Database.MigrateToLastVersion();
            
            AreaRegistration.RegisterAllAreas();

            //// TODO: dry
            DependencyResolver.SetResolver(new StructureMapDependencyResolver());
            FilterProviders.Providers.Add(new StructureMapFilterAttributeFilterProvider());
            
            //// TODO: dry
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            GlobalFilters.Filters.Add(new UnitOfWorkFilter());

            //// TODO: dry
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}