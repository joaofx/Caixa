namespace Testes.Unitarios
{
    using Core.Models;
    using Felice.TestFramework;
    using NBehave.Spec.NUnit;
    using NUnit.Framework;

    [TestFixture]
    public class FechamentoTests : UnitTest
    {
        [Test]
        public void Deve_criar_fechamento_batido()
        {
            var fechamento = new Fechamento(100, 200, 100, 200);
            
            fechamento.Batido.ShouldBeTrue();
            
            fechamento.DiferencaNoCaixa.ShouldEqual(0);
            fechamento.SaldoInformadoCaixa.ShouldEqual(100);

            fechamento.DiferencaNaConta.ShouldEqual(0);
            fechamento.SaldoInformadoConta.ShouldEqual(200);
        }

        [Test]
        public void Deve_criar_fechamento_com_diferenca_no_caixa()
        {
            var fechamento = new Fechamento(100, 200, 101.50m, 200);

            fechamento.Batido.ShouldBeFalse();

            fechamento.SomaDoCaixa.ShouldEqual(101.50m);
            fechamento.DiferencaNoCaixa.ShouldEqual(-1.50m);
            fechamento.SaldoInformadoCaixa.ShouldEqual(100);

            fechamento.SomaDaConta.ShouldEqual(200);
            fechamento.DiferencaNaConta.ShouldEqual(0);
            fechamento.SaldoInformadoConta.ShouldEqual(200);
        }

        [Test]
        public void Deve_criar_fechamento_com_diferenca_na_conta()
        {
            var fechamento = new Fechamento(100, 200, 100, 195.70m);

            fechamento.Batido.ShouldBeFalse();

            fechamento.SomaDoCaixa.ShouldEqual(100);
            fechamento.DiferencaNoCaixa.ShouldEqual(0);
            fechamento.SaldoInformadoCaixa.ShouldEqual(100);

            fechamento.SomaDaConta.ShouldEqual(195.70m);
            fechamento.DiferencaNaConta.ShouldEqual(4.3m);
            fechamento.SaldoInformadoConta.ShouldEqual(200);
        }

        [Test]
        public void Deve_criar_fechamento_com_diferenca_no_caixa_e_na_conta()
        {
            var fechamento = new Fechamento(100, 200, 94.30m, 202.90m);

            fechamento.Batido.ShouldBeFalse();

            fechamento.SomaDoCaixa.ShouldEqual(94.30m);
            fechamento.DiferencaNoCaixa.ShouldEqual(5.7m);
            fechamento.SaldoInformadoCaixa.ShouldEqual(100);

            fechamento.SomaDaConta.ShouldEqual(202.90m);
            fechamento.DiferencaNaConta.ShouldEqual(-2.9m);
            fechamento.SaldoInformadoConta.ShouldEqual(200);
        }
    }
}
