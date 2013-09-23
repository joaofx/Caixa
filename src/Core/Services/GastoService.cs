namespace Core.Services
{
    using Felice.Core;
    using Models;
    using Repositories;

    public class GastoService : IGastoService
    {
        private readonly IMovimentoRepository movimentoRepository;
        private readonly ITransacaoRepository transacaoRepository;

        public GastoService(
            IMovimentoRepository movimentoRepository, 
            ITransacaoRepository transacaoRepository)
        {
            this.movimentoRepository = movimentoRepository;
            this.transacaoRepository = transacaoRepository;
        }

        public Transacao Lancar(Conta conta, Categoria categoria, decimal valor, string descricao = "")
        {
            var movimento = this.movimentoRepository.GetAtual();

            var transacao = new Transacao(movimento)
            {
                Categoria = categoria,
                Conta = conta,
                Tipo = TipoTransacao.Gasto,
                Valor = valor.Negative(),
                Descricao = descricao
            };

            this.transacaoRepository.Save(transacao);

            return transacao;
        }
    }
}