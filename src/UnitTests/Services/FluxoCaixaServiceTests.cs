namespace UnitTests.Services
{
    using System;
    using System.Collections.ObjectModel;
    using Core.Models;
    using Core.Repositories;
    using Core.Services;
    using Felice.TestFramework;
    using NBehave.Spec.NUnit;
    using NUnit.Framework;
    using Rhino.Mocks;

    [TestFixture]
    public class FluxoCaixaServiceTests : MockedTest<FluxoCaixaService>
    {
        [Test]
        public void Deve_obter_fluxo_de_caixa()
        {
            var transacoes = new ReadOnlyCollection<Transacao>(new[]
            {
                this.NovaTransacao(DateTime.Parse("09/09/2013"), 15),
                this.NovaTransacao(DateTime.Parse("09/09/2013"), -5),

                this.NovaTransacao(DateTime.Parse("10/09/2013"), 100.59m),
                this.NovaTransacao(DateTime.Parse("10/09/2013"), 50.40m),
                this.NovaTransacao(DateTime.Parse("10/09/2013"), -61.55m),
                this.NovaTransacao(DateTime.Parse("10/09/2013"), -4m),

                this.NovaTransacao(DateTime.Parse("11/09/2013"), 508.01m),
                this.NovaTransacao(DateTime.Parse("11/09/2013"), 50.30m),
                this.NovaTransacao(DateTime.Parse("11/09/2013"), -55.99m),
                this.NovaTransacao(DateTime.Parse("11/09/2013"), -600m),
            });

            this.Get<ITransacaoRepository>()
                .Stub(x => x.AllByDate(DateTime.Parse("09/09/2013"), DateTime.Parse("11/09/2013")))
                .Return(transacoes);

            var fluxoDeCaixa = this.ClassUnderTest.Obter(
                DateTime.Parse("10/09/2013"), 
                DateTime.Parse("11/09/2013"));

            fluxoDeCaixa[0].Data.ShouldEqual(DateTime.Parse("10/09/2013"));
            fluxoDeCaixa[0].SaldoAnterior.ShouldEqual(10m);
            fluxoDeCaixa[0].Entradas.ShouldEqual(150.99m);
            fluxoDeCaixa[0].Saidas.ShouldEqual(-65.55m);
            fluxoDeCaixa[0].Saldo.ShouldEqual(95.44m);

            fluxoDeCaixa[1].Data.ShouldEqual(DateTime.Parse("11/09/2013"));
            fluxoDeCaixa[1].SaldoAnterior.ShouldEqual(95.44m);
            fluxoDeCaixa[1].Entradas.ShouldEqual(558.31m);
            fluxoDeCaixa[1].Saidas.ShouldEqual(-655.99m);
            fluxoDeCaixa[1].Saldo.ShouldEqual(-2.24m);
        }

        private Transacao NovaTransacao(DateTime data, decimal valor)
        {
            return new Transacao
            {
                Data = data,
                Valor = valor
            };
        }
    }
}
