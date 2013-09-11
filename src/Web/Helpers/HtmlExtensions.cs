namespace Web.Helpers
{
    using System.Web.Mvc;
    using Core.Models;

    public static class HtmlExtension
    {
        public static Movimento Movimento<T>(this WebViewPage<T> page)
        {
            return page.ViewBag.Movimento;
        }

        public static Movimento MovimentoAnterior<T>(this WebViewPage<T> page)
        {
            return page.ViewBag.MovimentoAnterior;
        }
    }
}