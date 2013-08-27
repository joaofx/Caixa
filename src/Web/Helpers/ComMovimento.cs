namespace Web.Helpers
{
    using System;
    using System.Web.Mvc;
    using Core.Repositories;
    using Felice.Core;

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ComMovimento : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var movimento = Dependency.Resolve<IMovimentoRepository>().GetAtual();

            if (movimento == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "Fechado",
                    ViewData = filterContext.Controller.ViewData
                };
            }
            else
            {
                filterContext.Controller.ViewBag.Movimento = movimento;    
            }
        }
    }
}
