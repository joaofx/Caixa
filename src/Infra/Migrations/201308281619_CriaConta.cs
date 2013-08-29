namespace Infra.Migrations
{
    using FluentMigrator;

    [Migration(201308281619)]
    public class CriaConta : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("conta")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("nome").AsString(64).Nullable()
                .WithColumn("saldo").AsDecimal().Nullable();
        }
    }
}
