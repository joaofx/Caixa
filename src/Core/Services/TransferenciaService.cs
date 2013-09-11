namespace Core.Services
{
    using System;
    using Felice.Core;
    using Models;
    using Repositories;

    public class TransferenciaService
    {
        private readonly IMovimentoRepository movimentoRepository;
        private readonly ICategoriaRepository categoriaRepository;
        private readonly ITransacaoRepository transacaoRepository;
        private readonly IContaRepository contaRepository;

        public TransferenciaService(
            IMovimentoRepository movimentoRepository, 
            ICategoriaRepository categoriaRepository, 
            ITransacaoRepository transacaoRepository, 
            IContaRepository contaRepository)
        {
            this.movimentoRepository = movimentoRepository;
            this.categoriaRepository = categoriaRepository;
            this.transacaoRepository = transacaoRepository;
            this.contaRepository = contaRepository;
        }

        public Transferencia Transferir(long contaOrigemId, long contaDestinoId, decimal valor)
        {
            if (contaOrigemId == contaDestinoId)
            {
                throw new RegraVioladaException("Conta de origem deve ser diferente da conta de destino");    
            }
            
            var contaOrigem = this.contaRepository.ById(contaOrigemId);
            var contaDestino = this.contaRepository.ById(contaDestinoId);

            var movimento = this.movimentoRepository.GetAtual();
            var categoriaTransferencia = this.categoriaRepository.GetTransferencia();

            var transacaoDebito = new Transacao(movimento)
            {
                Categoria = categoriaTransferencia,
                Conta = contaOrigem,
                Tipo = TipoTransacao.Transferencia,
                Valor = valor.Negative(),
                Descricao = "Transferência para " + contaDestino.Nome
            };

            var transacaoCredito = new Transacao(movimento)
            {
                Categoria = categoriaTransferencia,
                Conta = contaDestino,
                Tipo = TipoTransacao.Transferencia,
                Valor = valor.Positive(),
                Descricao = "Transferência vindo da " + contaOrigem.Nome
            };

            var transferencia = new Transferencia(transacaoDebito, transacaoCredito);

            this.transacaoRepository.Save(transferencia.TransacaoDebito);
            this.transacaoRepository.Save(transferencia.TransacaoCredito);

            return transferencia;
        }
    }
}