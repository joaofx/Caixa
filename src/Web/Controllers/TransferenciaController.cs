namespace Web.Controllers
{
    using System.Web.Mvc;
    using Core.Services;
    using Felice.Core;
    using Felice.Core.Mvc;
    using Helpers;
    using Infra.Repositories;
    using Web.Forms;

    public class TransferenciaController : Controller
    {
        private readonly ContaRepository contaRepository;
        private readonly TransferenciaService transferenciaService;

        public TransferenciaController(ContaRepository contaRepository, TransferenciaService transferenciaService)
        {
            this.contaRepository = contaRepository;
            this.transferenciaService = transferenciaService;
        }

        public ActionResult Nova()
        {
            //// TODO: tem que ter mais de uma conta
            //// TODO: tela abre mostrando contas diferentes nos campos contas
            return this.View("Editar", new EditarTransferenciaForm
            {
                Contas = this.contaRepository.Todos()
            });
        }

        [HttpPost]
        [ComMovimento]
        public ActionResult Editar(EditarTransferenciaForm form)
        {
            return this.Handle(form)
                .With(x => this.transferenciaService.Transferir(
                    form.ContaOrigemId.ToLong(),
                    form.ContaDestinoId.ToLong(),
                    form.Valor.ToDecimal2()))
                .OnSuccess(x => this.RedirectToAction("Index", "Transacao"), "Transferência salva com sucesso")
                .OnFailure(x =>
                {
                    x.Contas = this.contaRepository.Todos();
                    return this.View("Editar", x);
                });
        }
    }
}
