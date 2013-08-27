namespace Testes.Unitarios
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
        [Test]
        public void Deve_fechar_movimento_sem_diferenca()
        {
            var caixa = new Conta(Conta.CaixaId) { Saldo = 100 };
            var conta = new Conta(Conta.ContaCorrenteId) { Saldo = 200 };

            var movimento = new Movimento(DateTime.Parse("27/08/2013"));

            this.Get<IMovimentoRepository>()
                .GetAnterior()
                .Returns(movimento);

            this.Get<IContaRepository>()
                .ById(caixa.Id)
                .Returns(caixa);

            this.Get<IContaRepository>()
                .ById(conta.Id)
                .Returns(conta);

            this.Get<ITransacaoRepository>()
                .SumByConta(movimento, caixa.Id)
                .Returns(300);

            this.Get<ITransacaoRepository>()
                .SumByConta(movimento, conta.Id)
                .Returns(400);

            var fechamento = this.ClassUnderTest.Fechar(300, 400);

            fechamento.Batido.ShouldBeTrue();

            caixa.Saldo.ShouldEqual(400);
            conta.Saldo.ShouldEqual(600);

            movimento.Status.ShouldEqual(MovimentoStatus.Fechado);
            movimento.SaldoFinalCaixa.ShouldEqual(300);
            movimento.SaldoFinalConta.ShouldEqual(400);
        }
    }
}
