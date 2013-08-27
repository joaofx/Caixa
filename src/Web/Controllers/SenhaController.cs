using System.Web.Mvc;

namespace Web.Controllers
{
    using Felice.Core.Mvc;
    using Forms;

    public class SenhaController : Controller
    {
        public ActionResult Trocar()
        {
            return View(new TrocarSenhaForm());
        }

        [HttpPost]
        public ActionResult Trocar(TrocarSenhaForm form)
        {
            this.Success("Senha trocada com sucesso");
            return RedirectToAction("Index", "Home");
        }
    }
}
