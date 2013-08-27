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
    public class RecebimentoServiceTests : MockedTest<RecebimentoService>
    {
        [Test]
        public void Deve_receber()
        {
            var conta = new Conta(1);
            var categoria = new Categoria(2);
            var movimento = new Movimento(DateTime.Parse("22/08/2013"));

            this.Get<IMovimentoRepository>()
                .GetAtual()
                .Returns(movimento);

            var transacao = this.ClassUnderTest.Lancar(conta, categoria, 15.99m);

            transacao.Conta.ShouldEqual(conta);
            transacao.Categoria.ShouldEqual(categoria);
            transacao.Movimento.ShouldEqual(movimento);
            transacao.Data.ShouldEqual(DateTime.Parse("22/08/2013"));
            transacao.Valor.ShouldEqual(15.99m);
        }
    }
}
