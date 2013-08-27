namespace Core.Services
{
    using Core.Models;
    using Core.Repositories;

    public class FechaMovimentoService
    {
        private readonly IContaRepository contaRepository;
        private readonly IMovimentoRepository movimentoRepository;
        private readonly ICategoriaRepository categoriaRepository;
        private readonly ITransacaoRepository transacaoRepository;

        public FechaMovimentoService(
            IContaRepository contaRepository, 
            IMovimentoRepository movimentoRepository, 
            ICategoriaRepository categoriaRepository, 
            ITransacaoRepository transacaoRepository)
        {
            this.contaRepository = contaRepository;
            this.movimentoRepository = movimentoRepository;
            this.categoriaRepository = categoriaRepository;
            this.transacaoRepository = transacaoRepository;
        }

        public Fechamento Fechar(
            decimal saldoInformadoNoCaixa, 
            decimal saldoInformadoNaConta)
        {
            var movimento = this.movimentoRepository.GetAtual();
            var caixa = this.contaRepository.ById(Conta.CaixaId);
            var conta = this.contaRepository.ById(Conta.ContaCorrenteId);

            var somaDoCaixa = caixa.Saldo + this.transacaoRepository.SumByConta(movimento, caixa.Id);
            var somaDaConta = conta.Saldo + this.transacaoRepository.SumByConta(movimento, conta.Id);

            var fechamento = new Fechamento(
                saldoInformadoNoCaixa,
                saldoInformadoNaConta,
                somaDoCaixa,
                somaDaConta);

            if (fechamento.Batido)
            {
                movimento.Fechar(fechamento, MovimentoStatus.Fechado);
                this.movimentoRepository.Save(movimento);

                caixa.Saldo = somaDoCaixa;
                conta.Saldo = somaDaConta;

                this.contaRepository.Save(conta);
                this.contaRepository.Save(caixa);
            }

            return fechamento;
        }

        public Fechamento FecharComDiferenca(
            decimal saldoInformadoNoCaixa,
            decimal saldoInformadoNaConta)
        {
            var movimento = this.movimentoRepository.GetAtual();

            //// obtem diferencas
            var fechamento = this.Fechar(saldoInformadoNoCaixa, saldoInformadoNaConta);

            if (fechamento.Batido)
            {
                return fechamento;
            }

            var caixa = this.contaRepository.ById(Conta.CaixaId);
            var conta = this.contaRepository.ById(Conta.ContaCorrenteId);

            this.transacaoRepository.Save(new Transacao(movimento)
            {
                Categoria = this.categoriaRepository.ObterAjusteDeSaldo(),
                Conta = caixa,
                Tipo = fechamento.DiferencaNoCaixa > 0 ? TipoTransacao.Recebimento : TipoTransacao.Gasto,
                Valor = fechamento.DiferencaNoCaixa
            });

            this.transacaoRepository.Save(new Transacao(movimento)
            {
                Categoria = this.categoriaRepository.ObterAjusteDeSaldo(),
                Conta = conta,
                Tipo = fechamento.DiferencaNaConta > 0 ? TipoTransacao.Recebimento : TipoTransacao.Gasto,
                Valor = fechamento.DiferencaNaConta
            });

            movimento.Fechar(fechamento, MovimentoStatus.FechadoComDiferenca);
            this.movimentoRepository.Save(movimento);

            caixa.Saldo = saldoInformadoNoCaixa;
            conta.Saldo = saldoInformadoNaConta;

            this.contaRepository.Save(conta);
            this.contaRepository.Save(caixa);

            return fechamento;
        }
    }
}