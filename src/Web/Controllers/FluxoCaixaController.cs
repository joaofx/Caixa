namespace Web.Controllers
{
    using System.Collections.Generic;
    using Core.Models;
    using Core.Repositories;
    using Core.Services;
    using System.Web.Mvc;

    public class FluxoCaixaController : Controller
    {
        private readonly IMovimentoRepository movimentoRepository;
        private readonly AbreMovimentoService abreMovimentoService;
        private readonly FechaMovimentoService fechaMovimentoService;

        public FluxoCaixaController(IMovimentoRepository movimentoRepository, AbreMovimentoService abreMovimentoService, FechaMovimentoService fechaMovimentoService)
        {
            this.movimentoRepository = movimentoRepository;
            this.abreMovimentoService = abreMovimentoService;
            this.fechaMovimentoService = fechaMovimentoService;
        }

        public ActionResult Index()
        {
            var movimentos = this.movimentoRepository.Todos();
            var fluxos = new List<FluxoCaixa>();
            var saldoAnterior = 0;

            foreach (var movimento in movimentos)
            {
            }

            return View();
        }
    }
}
