namespace Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Infra.Repositories;

    public class PendenciasController : Controller
    {
        private readonly PendenciaRepository pendenciaRepository;

        public PendenciasController(PendenciaRepository pendenciaRepository)
        {
            this.pendenciaRepository = pendenciaRepository;
        }

        public ActionResult Index()
        {
            return View(this.pendenciaRepository.AllPendentes());
        }
    }
}
