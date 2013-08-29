namespace Infra.Migrations
{
    using FluentMigrator;

    [Migration(201308291701)]
    public class CriaMovimento : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("movimento")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("data").AsDateTime().NotNullable()
                .WithColumn("status").AsInt16().Nullable()
                .WithColumn("saldo_caixa").AsDecimal().Nullable()
                .WithColumn("saldo_conta").AsDecimal().Nullable()
                .WithColumn("diferenca_caixa").AsDecimal().Nullable()
                .WithColumn("diferenca_conta").AsDecimal().Nullable();
        }
    }
}
