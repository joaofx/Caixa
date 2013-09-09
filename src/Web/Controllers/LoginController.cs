namespace Web.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;
    using Core.Models;
    using Core.Services;
    using Felice.Mvc;
    using Forms;
    using Helpers;
    using Microsoft.Web.Mvc;

    public class LoginController : Controller
    {
        private readonly AutenticacaoService autenticacaoService;
        private readonly UserSession userSession;

        public LoginController(AutenticacaoService autenticacaoService, UserSession userSession)
        {
            this.autenticacaoService = autenticacaoService;
            this.userSession = userSession;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(new LoginForm());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(LoginForm form)
        {
            return this.Handle(form)
                .With(x =>
                {
                    this.autenticacaoService.Autenticar(form.Senha);
                    this.userSession.SetAuthenticationToken("1", false, new Usuario { Id = 1 });
                })
                .OnFailure(x => this.View(form))
                .OnSuccess(x => this.RedirectToAction<HomeController>(c => c.Index()));
        }

        [AllowAnonymous]
        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction(x => x.Index());
        }
    }
}
