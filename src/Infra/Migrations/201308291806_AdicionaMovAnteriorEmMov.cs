namespace Infra.Migrations
{
    using FluentMigrator;

    [Migration(201308291806)]
    public class AdicionaMovAnteriorEmMov : AutoReversingMigration
    {
        public override void Up()
        {
            this.Alter.Table("movimento")
                .AddColumn("movimento_anterior_id").AsInt64().Nullable();
        }
    }
}
