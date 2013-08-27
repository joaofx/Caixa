namespace Web.Maps
{
    using Core.Models;
    using FluentNHibernate.Mapping;

    public class TransacaoMap : ClassMap<Transacao>
    {
        public TransacaoMap()
        {
            Table("transacao");
            Id(x => x.Id).GeneratedBy.Native().Column("id");
            Map(x => x.Data).Column("data");
            Map(x => x.Valor).Column("valor");
            Map(x => x.Descricao).Column("descricao");
            Map(x => x.Tipo).Column("tipo");

            References(x => x.Conta).Column("conta_id");
            References(x => x.Categoria).Column("categoria_id");
            References(x => x.Movimento).Column("movimento_id");
        }
    }
}
