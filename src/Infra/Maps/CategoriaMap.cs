namespace Infra.Maps
{
    using Core.Models;
    using FluentNHibernate.Mapping;

    public class CategoriaMap : ClassMap<Categoria>
    {
        public CategoriaMap()
        {
            this.Table("categoria");
            this.Id(x => x.Id).GeneratedBy.Native().Column("id");
            this.Map(x => x.Nome).Column("nome");
            this.References(x => x.Parent).Column("categoria_id");
        }
    }
}