namespace IntegrationTests.Maps
{
    using System;
    using Core.Models;
    using Felice.TestFramework;
    using FluentNHibernate.Testing;
    using NUnit.Framework;

    [TestFixture]
    public class TransacaoMapTest : MappingTest
    {
        [Test]
        public void Deve_persistir()
        {
            this.Entity<Transacao>()
                .CheckReference(x => x.Categoria, new Categoria())
                .CheckReference(x => x.Conta, new Conta())
                .CheckReference(x => x.Movimento, new Movimento(DateTime.Parse("23/09/2013")))
                .CheckProperty(x => x.Data, DateTime.Parse("24/09/2013"))
                .CheckProperty(x => x.Tipo, TipoTransacao.Recebimento)
                .CheckProperty(x => x.Valor, 25.90m)
                .CheckProperty(x => x.Descricao, "Vendas")
                .VerifyTheMappings();
        }
    }
}
