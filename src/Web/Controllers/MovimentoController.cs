namespace Web.Controllers
{
    using Core.Repositories;
    using Core.Services;
    using Felice.Core;
    using Felice.Mvc;
    using Forms;
    using Helpers;
    using System.Web.Mvc;

    public class MovimentoController : Controller
    {
        private readonly IMovimentoRepository movimentoRepository;
        private readonly AbreMovimentoService abreMovimentoService;
        private readonly FechaMovimentoService fechaMovimentoService;

        public MovimentoController(IMovimentoRepository movimentoRepository, AbreMovimentoService abreMovimentoService, FechaMovimentoService fechaMovimentoService)
        {
            this.movimentoRepository = movimentoRepository;
            this.abreMovimentoService = abreMovimentoService;
            this.fechaMovimentoService = fechaMovimentoService;
        }

        public ActionResult Index()
        {
            ViewBag.Movimento = this.movimentoRepository.GetAtual();
            return View(this.movimentoRepository.Todos());
        }

        [ComMovimentoAnterior]
        public ActionResult Abrir()
        {
            var movimento = this.movimentoRepository.GetAtual();

            if (movimento != null)
            {
                this.Alert("Já existe um movimento aberto. Você deve fechar antes de abrir um novo movimento.");
                return View("Aberto", movimento);
            }

            return View(new AbrirMovimentoForm());
        }

        [HttpPost]
        [ComMovimentoAnterior]
        public ActionResult Abrir(AbrirMovimentoForm form)
        {
            return this.Handle(form)
                .With(x => this.abreMovimentoService.Abrir(x.DataNovoMovimento.ToDateTime()))
                .OnSuccess(x => this.RedirectToAction("Index", "Home"), "Novo movimento foi criado")
                .OnFailure(this.View);
        }

        [ComMovimento]
        public ActionResult Fechar()
        {
            return View(new FecharMovimentoForm());
        }

        public ActionResult Fechado()
        {
            return View();
        }

        [HttpPost]
        [ComMovimento]
        public ActionResult Fechar(FecharMovimentoForm form)
        {
            return this.Handle(form)
                .With(x => form.Fechamento = this.fechaMovimentoService.Fechar(
                    form.SaldoCaixa.ToDecimal2(),
                    form.SaldoConta.ToDecimal2()))
                .OnSuccess(x => x.Fechamento.Batido == false ? 
                    (ActionResult) this.View("Diferenca", x) : 
                    this.RedirectToAction("Fechado"))
                .OnFailure(this.View);
        }

        [HttpPost]
        public ActionResult FecharComDiferenca(FecharMovimentoForm form)
        {
            return this.Handle(form)
                .With(x => this.fechaMovimentoService.FecharComDiferenca(
                    x.SaldoCaixa.ToDecimal2(),
                    x.SaldoConta.ToDecimal2()))
                .OnSuccess(x => this.RedirectToAction("Fechado"))
                .OnFailure(x => this.View("Fechar", x));
        }
    }
}
