namespace UnitTests.Services
{
    using System;
    using Core.Models;
    using Core.Repositories;
    using Core.Services;
    using Felice.TestFramework;
    using NBehave.Spec.NUnit;
    using NUnit.Framework;
    using Rhino.Mocks;

    [TestFixture]
    public class FechaMovimentoServiceTests : MockedTest<FechaMovimentoService>
    {
        private Conta caixa;
        private Conta conta;
        private Movimento movimento;

        public override void Scenario()
        {
            this.caixa = new Conta(Conta.CaixaId) { Saldo = 100 };
            this.conta = new Conta(Conta.ItauId) { Saldo = 200 };
            this.movimento = new Movimento(DateTime.Parse("27/08/2013")) { Id = 30 };

            this.Get<IMovimentoRepository>()
                .Stub(x => x.GetAtual())
                .Return(this.movimento);

            this.Get<IContaRepository>()
                .Stub(x => x.ById(this.caixa.Id))
                .Return(this.caixa);

            this.Get<IContaRepository>()
                .Stub(x => x.ById(this.conta.Id))
                .Return(this.conta);

            this.Get<ITransacaoRepository>()
                .Stub(x => x.SumByConta(this.movimento, this.caixa.Id))
                .Return(200);

            this.Get<ITransacaoRepository>()
                .Stub(x => x.SumByConta(this.movimento, this.conta.Id))
                .Return(200);
        }

        [Test]
        public void Deve_fechar_movimento_sem_diferenca()
        {
            var fechamento = this.ClassUnderTest.Fechar(300, 400);

            fechamento.Batido.ShouldBeTrue();

            this.caixa.Saldo.ShouldEqual(300);
            this.conta.Saldo.ShouldEqual(400);

            this.movimento.Status.ShouldEqual(MovimentoStatus.Fechado);
            this.movimento.SaldoFinalCaixa.ShouldEqual(300);
            this.movimento.SaldoFinalConta.ShouldEqual(400);
            this.movimento.DiferencaNoCaixa.ShouldEqual(0);
            this.movimento.DiferencaNaConta.ShouldEqual(0);

            this.Get<IMovimentoRepository>()
                .AssertWasCalled(x => x.Save(this.movimento));

            this.Get<IContaRepository>()
                .AssertWasCalled(x => x.Save(this.conta));

            this.Get<IContaRepository>()
                .AssertWasCalled(x => x.Save(this.caixa));
        }

        [Test]
        public void Deve_tentar_fechar_movimento_com_diferenca()
        {
            var fechamento = this.ClassUnderTest.Fechar(350, 460);

            fechamento.Batido.ShouldBeFalse();

            this.caixa.Saldo.ShouldEqual(100);
            this.conta.Saldo.ShouldEqual(200);

            this.movimento.Status.ShouldEqual(MovimentoStatus.Aberto);
            this.movimento.SaldoFinalCaixa.ShouldEqual(0);
            this.movimento.SaldoFinalConta.ShouldEqual(0);
            this.movimento.DiferencaNoCaixa.ShouldEqual(0);
            this.movimento.DiferencaNaConta.ShouldEqual(0);

            this.Get<IMovimentoRepository>()
                .AssertWasNotCalled(x => x.Save(this.movimento));

            this.Get<IContaRepository>()
                .AssertWasNotCalled(x => x.Save(this.conta));

            this.Get<IContaRepository>()
                .AssertWasNotCalled(x => x.Save(this.caixa));
        }

        [Test]
        public void Deve_fechar_movimento_com_diferenca()
        {
            this.ClassUnderTest.FecharComDiferenca(350, 460);

            this.caixa.Saldo.ShouldEqual(350);
            this.conta.Saldo.ShouldEqual(460);

            this.movimento.Status.ShouldEqual(MovimentoStatus.FechadoComDiferenca);
            this.movimento.SaldoFinalCaixa.ShouldEqual(350);
            this.movimento.SaldoFinalConta.ShouldEqual(460);
            this.movimento.DiferencaNoCaixa.ShouldEqual(50);
            this.movimento.DiferencaNaConta.ShouldEqual(60);

            this.Get<IMovimentoRepository>()
                .AssertWasCalled(x => x.Save(this.movimento));

            this.Get<IContaRepository>()
                .AssertWasCalled(x => x.Save(this.conta));

            this.Get<IContaRepository>()
                .AssertWasCalled(x => x.Save(this.caixa));
        }
    }
}
