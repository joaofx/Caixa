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
    public class RecebimentoServiceTests : MockedTest<RecebimentoService>
    {
        [Test]
        public void Deve_receber()
        {
            var conta = new Conta(1);
            var categoria = new Categoria(2);
            var movimento = new Movimento(DateTime.Parse("22/08/2013"));

            this.Get<IMovimentoRepository>()
                .Stub(x => x.GetAtual())
                .Return(movimento);

            var transacao = this.ClassUnderTest.Lancar(conta, categoria, 15.99m, "compras");

            transacao.Conta.ShouldEqual(conta);
            transacao.Categoria.ShouldEqual(categoria);
            transacao.Movimento.ShouldEqual(movimento);
            transacao.Data.ShouldEqual(DateTime.Parse("22/08/2013"));
            transacao.Valor.ShouldEqual(15.99m);
            transacao.Descricao.ShouldEqual("compras");
        }
    }
}
