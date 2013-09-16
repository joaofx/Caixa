namespace Web.Controllers
{
    using System;
    using Core.Services;
    using System.Web.Mvc;

    public class FluxoCaixaController : Controller
    {
        private readonly FluxoCaixaService fluxoCaixaService;

        public FluxoCaixaController(FluxoCaixaService fluxoCaixaService)
        {
            this.fluxoCaixaService = fluxoCaixaService;
        }

        public ActionResult Index()
        {
            return View(this.fluxoCaixaService.Obter(
                DateTime.Today.AddDays(-30), 
                DateTime.Today));
        }
    }
}
