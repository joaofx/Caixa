using System.Web.Mvc;

namespace Web.Controllers
{
    using Felice.Core.Data;
    using Felice.Core.Mvc;
    using Infra.Repositories;

    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Contas()
        {
            new ContaRepository().Seed();
            this.Success("Contas carregadas");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Categorias()
        {
            new CategoriaRepository().Seed();
            this.Success("Categorias carregadas");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Schema()
        {
            Database.UpdateSchema();
            this.Success("Schema atualizado");
            return RedirectToAction("Index");
        }
    }
}
