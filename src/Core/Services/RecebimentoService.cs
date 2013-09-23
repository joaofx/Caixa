namespace Core.Services
{
    using Felice.Core;
    using Models;
    using Repositories;

    public class RecebimentoService : IRecebimentoService
    {
        private readonly IMovimentoRepository movimentoRepository;
        private readonly ITransacaoRepository transacaoRepository;

        public RecebimentoService(IMovimentoRepository movimentoRepository, ITransacaoRepository transacaoRepository)
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
                Tipo = TipoTransacao.Recebimento,
                Valor = valor.Positive(),
                Descricao = descricao
            };

            this.transacaoRepository.Save(transacao);
            
            return transacao;
        }
    }
}