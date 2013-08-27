namespace Web
{
    using App_Start;
    using Felice.Core;
    using Felice.Core.Data;
    using Felice.Core.IoC;
    using Felice.Core.Mvc;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            FeliceCore.Initialize();

            Database.Initialize();
            Database.UpdateSchema();

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