namespace Testes.Integracao.Maps
{
    using System;
    using Core.Models;
    using Felice.TestFramework;
    using FluentNHibernate.Testing;
    using NUnit.Framework;

    [TestFixture]
    public class MovimentoMapTest : MappingTest
    {
        [Test]
        public void Deve_persistir()
        {
            this.Entity<Movimento>()
                .CheckReference(x => x.MovimentoAnterior, new Movimento(new DateTime(2013, 08, 28)))
                .CheckProperty(x => x.Data, new DateTime(2013, 08, 29))
                .CheckProperty(x => x.Status, MovimentoStatus.FechadoComDiferenca)
                .CheckProperty(x => x.DiferencaNaConta, 15678.51m)
                .CheckProperty(x => x.DiferencaNoCaixa, -45678.51m)
                .CheckProperty(x => x.SaldoFinalConta, 99999.99m)
                .CheckProperty(x => x.SaldoFinalCaixa, -99999.99m)
                .VerifyTheMappings();
        }
    }
}
