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
    public class TransferenciaServiceTests : MockedTest<TransferenciaService>
    {
        private Conta conta1;
        private Conta conta2;
        private Movimento movimento;
        private Categoria categoria;

        public override void Scenario()
        {
            this.conta1 = new Conta(1);
            this.conta2 = new Conta(2);
            this.movimento = new Movimento(DateTime.Parse("22/08/2013"));
            this.categoria = new Categoria(99);

            this.Get<IContaRepository>()
                .Stub(x => x.ById(this.conta1.Id))
                .Return(this.conta1);

            this.Get<IContaRepository>()
                .Stub(x => x.ById(this.conta2.Id))
                .Return(this.conta2);

            this.Get<IMovimentoRepository>()
                .Stub(x => x.GetAtual())
                .Return(this.movimento);

            this.Get<ICategoriaRepository>()
                .Stub(x => x.GetTransferencia())
                .Return(this.categoria);
        }

        [Test]
        public void Deve_transferir()
        {
            var transferencia = this.ClassUnderTest.Transferir(this.conta1.Id, this.conta2.Id, 55.8m);

            transferencia.TransacaoDebito.Conta.ShouldEqual(this.conta1);
            transferencia.TransacaoDebito.Categoria.ShouldEqual(this.categoria);
            transferencia.TransacaoDebito.Movimento.ShouldEqual(this.movimento);
            transferencia.TransacaoDebito.Data.ShouldEqual(DateTime.Parse("22/08/2013"));
            transferencia.TransacaoDebito.Valor.ShouldEqual(-55.8m);

            transferencia.TransacaoCredito.Conta.ShouldEqual(this.conta2);
            transferencia.TransacaoCredito.Categoria.ShouldEqual(this.categoria);
            transferencia.TransacaoCredito.Movimento.ShouldEqual(this.movimento);
            transferencia.TransacaoCredito.Data.ShouldEqual(DateTime.Parse("22/08/2013"));
            transferencia.TransacaoCredito.Valor.ShouldEqual(55.8m);
        }

        [Test]
        [ExpectedException(typeof(RegraVioladaException))]
        public void Deve_disparar_excecao_se_conta_de_destino_e_origem_forem_iguais()
        {
            this.ClassUnderTest.Transferir(this.conta1.Id, this.conta1.Id, 55.8m);
        }
    }
}
