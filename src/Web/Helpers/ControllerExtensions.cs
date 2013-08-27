namespace Web.Helpers
{
    using System.Web.Mvc;
    using Core.Models;

    public static class ControllerExtensions
    {
        public static Movimento Movimento(this Controller controller)
        {
            return controller.ViewBag.Movimento;
        }

        public static Movimento MovimentoAnterior(this Controller controller)
        {
            return controller.ViewBag.MovimentoAnterior;
        }
    }
}