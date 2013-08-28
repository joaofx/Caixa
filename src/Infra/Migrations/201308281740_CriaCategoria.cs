namespace Infra.Migrations
{
    using FluentMigrator;

    [Migration(201308281740)]
    public class CriaCategoria : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("categoria")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("nome").AsString(64)
                .WithColumn("categoria_id").AsInt64();
        }
    }
}
