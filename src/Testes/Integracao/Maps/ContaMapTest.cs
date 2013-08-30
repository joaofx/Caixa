namespace Testes.Integracao.Maps
{
    using Core.Models;
    using Felice.TestFramework;
    using FluentNHibernate.Testing;
    using NUnit.Framework;

    [TestFixture]
    public class ContaMapTest : MappingTest
    {
        [Test]
        public void Deve_persistir()
        {
            this.Entity<Conta>()
                .CheckProperty(x => x.Nome, "Itau")
                .CheckProperty(x => x.Saldo, 550.99m)
                .VerifyTheMappings();
        }
    }
}
