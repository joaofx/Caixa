namespace Infra.Maps
{
    using Core.Models;
    using FluentNHibernate.Mapping;

    public class ContaMap : ClassMap<Conta>
    {
        public ContaMap()
        {
            this.Table("conta");
            this.Id(x => x.Id).GeneratedBy.Native().Column("id");
            this.Map(x => x.Nome).Column("nome");
            this.Map(x => x.Saldo).Column("saldo");
        }
    }
}