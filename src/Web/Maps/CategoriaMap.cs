namespace Web.Maps
{
    using Core.Models;
    using FluentNHibernate.Mapping;

    public class CategoriaMap : ClassMap<Categoria>
    {
        public CategoriaMap()
        {
            Table("categoria");
            Id(x => x.Id).GeneratedBy.Native().Column("id");
            Map(x => x.Nome).Column("nome");
            References(x => x.Parent).Column("categoria_id");
        }
    }
}