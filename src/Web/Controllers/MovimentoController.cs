namespace Web.Controllers
{
    using Core.Repositories;
    using Core.Services;
    using Felice.Core;
    using Felice.Core.Mvc;
    using Forms;
    using Helpers;
    using System.Web.Mvc;

    public class MovimentoController : Controller
    {
        private readonly MovimentoServico movimentoServico;
        private readonly IMovimentoRepository movimentoRepository;
        private readonly AbreMovimentoService abreMovimentoService;
        private readonly FechaMovimentoService fechaMovimentoService;

        public MovimentoController(MovimentoServico movimentoServico, IMovimentoRepository movimentoRepository, AbreMovimentoService abreMovimentoService, FechaMovimentoService fechaMovimentoService)
        {
            this.movimentoServico = movimentoServico;
            this.movimentoRepository = movimentoRepository;
            this.abreMovimentoService = abreMovimentoService;
            this.fechaMovimentoService = fechaMovimentoService;
        }

        [ComMovimento]
        public ActionResult Index()
        {
            return View(this.movimentoServico.Todos());
        }

        [ComMovimentoAnterior]
        public ActionResult Abrir()
        {
            var movimento = this.movimentoRepository.GetAtual();

            if (movimento != null)
            {
                this.Alert("Já existe um movimento aberto. Você deve fechar antes de abrir um novo movimento.");
                return View("Aberto", this.Movimento());
            }

            return View(new AbrirMovimentoForm
            {
                MovimentoAnterior = this.MovimentoAnterior()
            });
        }

        [HttpPost]
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
            if (this.Movimento() == null)
            {
                return View("Fechado");
            }

            return View(new FecharMovimentoForm());
        }

        [HttpPost]
        public ActionResult Fechar(FecharMovimentoForm form)
        {
            return this.Handle(form)
                .With(x =>
                {
                    form.Fechamento = this.fechaMovimentoService.Fechar(
                        form.SaldoCaixa.ToDecimal2(),
                        form.SaldoConta.ToDecimal2());
                })
                .OnSuccess(x =>
                {
                    if (x.Fechamento.Batido == false)
                    {
                        return this.View("Diferenca", x);
                    }

                    return this.RedirectToAction("Index");
                })
                .OnFailure(this.View);
        }

        [HttpPost]
        public ActionResult FecharComDiferenca(FecharMovimentoForm form)
        {
            return this.Handle(form)
                .With(x =>
                {
                    this.fechaMovimentoService.FecharComDiferenca(
                        x.SaldoCaixa.ToDecimal2(),
                        x.SaldoConta.ToDecimal2());
                })
                .OnSuccess(x => this.RedirectToAction("Index"), "Movimento foi fechado com diferença")
                .OnFailure(x => this.View("Fechar", x));
        }
    }
}
