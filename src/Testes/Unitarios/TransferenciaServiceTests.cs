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
                .ById(contaOrigem.Id)
                .Returns(contaOrigem);

            this.Get<IContaRepository>()
                .ById(contaDestino.Id)
                .Returns(contaDestino);

            this.Get<IMovimentoRepository>()
                .GetAtual()
                .Returns(movimento);

            this.Get<ICategoriaRepository>()
                .GetTransferencia()
                .Returns(categoria);

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
