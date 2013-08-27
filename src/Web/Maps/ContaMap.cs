namespace Web.Maps
{
    using Core.Models;
    using FluentNHibernate.Mapping;

    public class ContaMap : ClassMap<Conta>
    {
        public ContaMap()
        {
            Table("conta");
            Id(x => x.Id).GeneratedBy.Native().Column("id");
            Map(x => x.Nome).Column("nome");
            Map(x => x.Saldo).Column("saldo");
        }
    }
}