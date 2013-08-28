namespace Infra.Migrations
{
    using System;
    using FluentMigrator;

    public class MigrationOptions : IMigrationProcessorOptions
    {
        public bool PreviewOnly { get; set; }
        public int Timeout { get; set; }

        public string ProviderSwitches
        {
            get
            {
                return String.Empty;
            }
        }
    }
}