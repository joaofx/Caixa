namespace Core.Services
{
    using Core.Models;
    using Repositories;

    public class SalvaTransacaoService
    {
        private readonly IGastoService gastoService;
        private readonly IContaRepository contaRepository;
        private readonly ICategoriaRepository categoriaRepository;
        private readonly IRecebimentoService recebimentoService;

        public SalvaTransacaoService(
            IGastoService gastoService,
            IContaRepository contaRepository, 
            ICategoriaRepository categoriaRepository, 
            IRecebimentoService recebimentoService)
        {
            this.gastoService = gastoService;
            this.contaRepository = contaRepository;
            this.categoriaRepository = categoriaRepository;
            this.recebimentoService = recebimentoService;
        }

        public void Processar(TipoTransacao tipo, long contaId, long categoriaId, decimal valor, string descricao = "")
        {
            var conta = this.contaRepository.ById(contaId);
            var categoria = this.categoriaRepository.ById(categoriaId);

            if (tipo == TipoTransacao.Gasto)
            {
                this.gastoService.Lancar(conta, categoria, valor, descricao);
            }
            else if (tipo == TipoTransacao.Recebimento)
            {
                this.recebimentoService.Lancar(conta, categoria, valor, descricao);
            }

            // TODO: throw
        }
    }
}