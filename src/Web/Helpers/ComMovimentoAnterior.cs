namespace Web.Helpers
{
    using System;
    using System.Web.Mvc;
    using Core.Repositories;
    using Felice.Core;

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ComMovimentoAnterior : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.MovimentoAnterior = 
                Dependency.Resolve<IMovimentoRepository>().GetAnterior();
        }
    }
}
