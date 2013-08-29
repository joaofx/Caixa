namespace Infra.Migrations
{
    using FluentMigrator;

    [Migration(201308291704)]
    public class CriaTransacao : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("transacao")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("data").AsDateTime().Nullable()
                .WithColumn("valor").AsDecimal().Nullable()
                .WithColumn("descricao").AsString(256).Nullable()
                .WithColumn("tipo").AsInt16().Nullable()
                .WithColumn("conta_id").AsInt64().Nullable()
                .WithColumn("categoria_id").AsInt64().Nullable()
                .WithColumn("movimento_id").AsInt64().Nullable()
                .WithColumn("diferenca_conta").AsDecimal().Nullable();
        }
    }
}
