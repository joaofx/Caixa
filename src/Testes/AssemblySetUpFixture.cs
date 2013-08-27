namespace Testes
{
    using Felice.Core;
    using Felice.Core.Data;
    using NUnit.Framework;

    [SetUpFixture]
    public class AssemblySetUpFixture
    {
        [SetUp]
        public void Setup()
        {
            FeliceCore.Initialize();

            Database.Initialize();
            Database.UpdateSchema();
        }
    }
}
