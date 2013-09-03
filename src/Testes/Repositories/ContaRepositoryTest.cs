namespace IntegrationTests.Repositories
{
    using Core.Models;
    using Felice.TestFramework;
    using Infra.Repositories;
    using NUnit.Framework;

    [TestFixture]
    public class ContaRepositoryTest : RepositoryTest<Conta, ContaRepository>
    {
        public override Conta CreateEntity()
        {
            return new Conta();
        }
    }
}