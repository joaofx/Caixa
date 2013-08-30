namespace Testes.Unitarios.Services
{
    using System;
    using Core.Models;
    using Core.Repositories;
    using Core.Services;
    using Felice.TestFramework;
    using NBehave.Spec.NUnit;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class FechaMovimentoServiceTests : MockedTest<FechaMovimentoService>
    {
        private Conta caixa;
        private Conta conta;
        private Movimento movimento;

        public override void Scenario()
        {
            this.caixa = new Conta(Conta.CaixaId) { Saldo = 100 };
            this.conta = new Conta(Conta.ContaCorrenteId) { Saldo = 200 };
            this.movimento = new Movimento(DateTime.Parse("27/08/2013")) { Id = 30 };

            this.Get<IMovimentoRepository>()
                .GetAtual()
                .Returns(movimento);

            this.Get<IContaRepository>()
                .ById(this.caixa.Id)
                .Returns(this.caixa);

            this.Get<IContaRepository>()
                .ById(this.conta.Id)
                .Returns(this.conta);

            this.Get<ITransacaoRepository>()
                .SumByConta(this.movimento, caixa.Id)
                .Returns(200);

            this.Get<ITransacaoRepository>()
                .SumByConta(this.movimento, conta.Id)
                .Returns(200);
        }

        [Test]
        public void Deve_fechar_movimento_sem_diferenca()
        {
            var fechamento = this.ClassUnderTest.Fechar(300, 400);

            fechamento.Batido.ShouldBeTrue();

            caixa.Saldo.ShouldEqual(300);
            conta.Saldo.ShouldEqual(400);

            this.movimento.Status.ShouldEqual(MovimentoStatus.Fechado);
            this.movimento.SaldoFinalCaixa.ShouldEqual(300);
            this.movimento.SaldoFinalConta.ShouldEqual(400);
            this.movimento.DiferencaNoCaixa.ShouldEqual(0);
            this.movimento.DiferencaNaConta.ShouldEqual(0);

            this.Get<IMovimentoRepository>()
                .Received()
                .Save(this.movimento);

            this.Get<IContaRepository>()
                .Received()
                .Save(conta);

            this.Get<IContaRepository>()
                .Received()
                .Save(caixa);
        }

        [Test]
        public void Deve_tentar_fechar_movimento_com_diferenca()
        {
            var fechamento = this.ClassUnderTest.Fechar(350, 460);

            fechamento.Batido.ShouldBeFalse();

            this.caixa.Saldo.ShouldEqual(100);
            this.conta.Saldo.ShouldEqual(200);

            movimento.Status.ShouldEqual(MovimentoStatus.Aberto);
            movimento.SaldoFinalCaixa.ShouldEqual(0);
            movimento.SaldoFinalConta.ShouldEqual(0);
            movimento.DiferencaNoCaixa.ShouldEqual(0);
            movimento.DiferencaNaConta.ShouldEqual(0);

            this.Get<IMovimentoRepository>()
                .DidNotReceive()
                .Save(movimento);

            this.Get<IContaRepository>()
                .DidNotReceive()
                .Save(this.conta);

            this.Get<IContaRepository>()
                .DidNotReceive()
                .Save(this.caixa);
        }

        [Test]
        public void Deve_fechar_movimento_com_diferenca()
        {
            this.ClassUnderTest.FecharComDiferenca(350, 460);

            this.caixa.Saldo.ShouldEqual(350);
            this.conta.Saldo.ShouldEqual(460);

            movimento.Status.ShouldEqual(MovimentoStatus.FechadoComDiferenca);
            movimento.SaldoFinalCaixa.ShouldEqual(350);
            movimento.SaldoFinalConta.ShouldEqual(460);
            movimento.DiferencaNoCaixa.ShouldEqual(50);
            movimento.DiferencaNaConta.ShouldEqual(60);

            this.Get<IMovimentoRepository>()
                .Received()
                .Save(this.movimento);

            this.Get<IContaRepository>()
                .Received()
                .Save(conta);

            this.Get<IContaRepository>()
                .Received()
                .Save(caixa);
        }
    }
}
