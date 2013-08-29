namespace Testes
{
    using Felice.Core;
    using Felice.Data;
    using NUnit.Framework;

    [SetUpFixture]
    public class AssemblySetUpFixture
    {
        [SetUp]
        public void Setup()
        {
            FeliceCore.Initialize();

            Database.Initialize();
            Database.MigrateToLastVersion();
        }
    }
}
