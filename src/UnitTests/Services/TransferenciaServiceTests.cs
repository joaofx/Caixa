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
        [Test]
        public void Deve_receber()
        {
            var contaOrigem = new Conta(1);
            var contaDestino = new Conta(2);
            var movimento = new Movimento(DateTime.Parse("22/08/2013"));
            var categoria = new Categoria(99);

            this.Get<IContaRepository>()
                .Stub(x => x.ById(contaOrigem.Id))
                .Return(contaOrigem);

            this.Get<IContaRepository>()
                .Stub(x => x.ById(contaDestino.Id))
                .Return(contaDestino);

            this.Get<IMovimentoRepository>()
                .Stub(x => x.GetAtual())
                .Return(movimento);

            this.Get<ICategoriaRepository>()
                .Stub(x => x.GetTransferencia())
                .Return(categoria);

            var transferencia = this.ClassUnderTest.Transferir(contaOrigem.Id, contaDestino.Id, 55.8m);

            transferencia.TransacaoDebito.Conta.ShouldEqual(contaOrigem);
            transferencia.TransacaoDebito.Categoria.ShouldEqual(categoria);
            transferencia.TransacaoDebito.Movimento.ShouldEqual(movimento);
            transferencia.TransacaoDebito.Data.ShouldEqual(DateTime.Parse("22/08/2013"));
            transferencia.TransacaoDebito.Valor.ShouldEqual(-55.8m);

            transferencia.TransacaoCredito.Conta.ShouldEqual(contaDestino);
            transferencia.TransacaoCredito.Categoria.ShouldEqual(categoria);
            transferencia.TransacaoCredito.Movimento.ShouldEqual(movimento);
            transferencia.TransacaoCredito.Data.ShouldEqual(DateTime.Parse("22/08/2013"));
            transferencia.TransacaoCredito.Valor.ShouldEqual(55.8m);
        }
    }
}
