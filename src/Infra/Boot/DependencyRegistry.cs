namespace Infra.Boot
{
    using Felice.Data;
    using Maps;
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

            Database.AddMappings(typeof(MovimentoMap).Assembly);
        }
    }
}
