namespace UnitTests.Services
{
    using System;
    using Core.Models;
    using Core.Repositories;
    using Core.Services;
    using Felice.TestFramework;
    using NUnit.Framework;
    using Rhino.Mocks;

    [TestFixture]
    public class SalvaTransacaoServiceTests : MockedTest<SalvaTransacaoService>
    {
        private Conta conta;
        private Categoria categoria;

        public override void Scenario()
        {
            this.conta = new Conta(1);
            this.categoria = new Categoria(2);

            this.Get<IContaRepository>()
                .Stub(x => x.ById(1))
                .Return(this.conta);

            this.Get<ICategoriaRepository>()
                .Stub(x => x.ById(2))
                .Return(this.categoria);
        }

        [Test]
        public void Deve_receber()
        {
            this.ClassUnderTest
                .Processar(TipoTransacao.Recebimento, this.conta.Id, this.categoria.Id, 15.99m, "vendas");

            this.Get<IRecebimentoService>()
                .AssertWasCalled(x => x.Lancar(this.conta, this.categoria, 15.99m, "vendas"));
        }

        [Test]
        public void Deve_gastar()
        {
            this.ClassUnderTest
                .Processar(TipoTransacao.Gasto, this.conta.Id, this.categoria.Id, 30.15m, "compras");

            this.Get<IGastoService>()
                .AssertWasCalled(x => x.Lancar(this.conta, this.categoria, 30.15m, "compras"));
        }
    }
}
