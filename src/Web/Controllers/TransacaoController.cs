namespace Web.Controllers
{
    using System.Web.Mvc;
    using Core.Models;
    using Core.Services;
    using Felice.Core;
    using Felice.Mvc;
    using Helpers;
    using Infra.Repositories;
    using ViewModels;
    using Web.Forms;

    public class TransacaoController : Controller
    {
        private readonly TransacaoRepository transacaoRepository;
        private readonly ContaRepository contaRepository;
        private readonly SalvaTransacaoService salvaTransacaoService;
        private readonly MovimentoRepository movimentoRepository;

        public TransacaoController(TransacaoRepository transacaoRepository, ContaRepository contaRepository, SalvaTransacaoService salvaTransacaoService, MovimentoRepository movimentoRepository)
        {
            this.transacaoRepository = transacaoRepository;
            this.contaRepository = contaRepository;
            this.salvaTransacaoService = salvaTransacaoService;
            this.movimentoRepository = movimentoRepository;
        }

        [ComMovimento]
        public ActionResult Index(int conta = 0)
        {
            var transacoes = this.transacaoRepository.AllByMovimento(this.Movimento(), conta);

            return View(new TransacaoIndexView
            {
                Contas = this.contaRepository.Todos(),
                MovimentoAtual = this.Movimento(),
                Transacoes = transacoes
            });
        }

        [ComMovimento]
        public ActionResult Nova(TipoTransacao tipo)
        {
            return this.View("Editar", new EditarTransacaoForm
            {
                Tipo = tipo,
                Contas = new ContaRepository().Todos(),
                Categorias = new CategoriaRepository().Hierarquia()
            });
        }

        [ComMovimento]
        public ActionResult Editar(long id)
        {
            var transacao = this.transacaoRepository.ById(id);
            return this.View(EditarTransacaoForm.FromModel(transacao));
        }

        [HttpPost]
        [ComMovimento]
        public ActionResult Editar(EditarTransacaoForm form)
        {
            return this.Handle(form)
                .With(x => this.salvaTransacaoService.Processar(
                    form.Tipo,
                    form.ContaId.ToLong(),
                    form.CategoriaId.ToLong(),
                    form.Valor.ToDecimal2(),
                    form.Descricao))
                .OnSuccess(x => this.RedirectToAction("Index"), "Transação salva com sucesso")
                .OnFailure(x =>
                {
                    x.Contas = new ContaRepository().Todos();
                    x.Categorias = new CategoriaRepository().Hierarquia();
                    return this.View("Editar", x);
                });
        }

        [HttpPost]
        public ActionResult Apagar(long id)
        {
            return this.Handle(id)
                .With(x => this.transacaoRepository.DeleteById(x))
                .OnSuccess(x => this.RedirectToAction("Index"), "Transação apagada com sucesso")
                .OnFailure(x => this.RedirectToAction("Index"));
        }

        public ActionResult Listar(int movimento)
        {
            var movimentoById = @ViewBag.Movimento = this.movimentoRepository.ById(movimento);
            var transacoes = this.transacaoRepository.AllByMovimento(movimentoById);
            return View(transacoes);
        }
    }
}
