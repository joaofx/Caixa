namespace Infra.Migrations
{
    using System.Reflection;
    using Felice.Core;
    using Felice.Core.Data;
    using FluentMigrator.Runner;
    using FluentMigrator.Runner.Announcers;
    using FluentMigrator.Runner.Initialization;
    using Maps;

    public static class Runner
    {
        public static void MigrateToLatest()
        {
            // var announcer = new NullAnnouncer();
            var announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
            var assembly = Assembly.GetExecutingAssembly();

            var migrationContext = new RunnerContext(announcer)
            {
                Namespace = typeof(CriaConta).Namespace
            };

            var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
            var factory = new FluentMigrator.Runner.Processors.Postgres.PostgresProcessorFactory();
            var processor = factory.Create(SettingsConfig.DatabaseConnectionString, announcer, options);
            var runner = new MigrationRunner(assembly, migrationContext, processor);

            runner.MigrateUp();
        }
    }
}
