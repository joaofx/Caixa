namespace Testes.Integracao
{
    using Core.Models;
    using Felice.TestFramework;
    using FluentNHibernate.Testing;
    using NUnit.Framework;

    public class CategoriaMapTest : MappingTest
    {
        [Test]
        public void Deve_persistir()
        {
            this.Entity<Categoria>()
                .CheckReference(x => x.Parent, new Categoria())
                .CheckProperty(x => x.Nome, "Itau")
                .VerifyTheMappings();
        }
    }
}
