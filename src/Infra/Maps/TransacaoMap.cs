namespace Infra.Maps
{
    using Core.Models;
    using FluentNHibernate.Mapping;

    public class TransacaoMap : ClassMap<Transacao>
    {
        public TransacaoMap()
        {
            this.Table("transacao");
            this.Id(x => x.Id).GeneratedBy.Native().Column("id");
            this.Map(x => x.Data).Column("data");
            this.Map(x => x.Valor).Column("valor");
            this.Map(x => x.Descricao).Column("descricao");
            this.Map(x => x.Tipo).Column("tipo");

            this.References(x => x.Conta).Column("conta_id");
            this.References(x => x.Categoria).Column("categoria_id");
            this.References(x => x.Movimento).Column("movimento_id");
        }
    }
}
