namespace IntegrationTests.Boot
{
    using Felice.TestFramework;
    using StructureMap.Configuration.DSL;

    public class DependencyRegistry : Registry
    {
        public DependencyRegistry()
        {
            this.Scan(x =>
            {
                x.AssemblyContainingType<DependencyRegistry>();
                x.WithDefaultConventions();
            });

            For<IDatabaseCleaner>().Use<DatabaseCleaner>();
        }
    }
}
