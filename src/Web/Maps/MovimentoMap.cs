namespace Web.Maps
{
    using Core.Models;
    using FluentNHibernate.Mapping;

    public class MovimentoMap : ClassMap<Movimento>
    {
        public MovimentoMap()
        {
            Table("movimento");
            Id(x => x.Id).GeneratedBy.Native().Column("id");
            Map(x => x.Data).Column("data");
            Map(x => x.SaldoFinalCaixa).Column("saldo_caixa");
            Map(x => x.SaldoFinalConta).Column("saldo_conta");
            Map(x => x.Status).Column("status");
            Map(x => x.DiferencaNoCaixa).Column("diferenca_caixa");
            Map(x => x.DiferencaNaConta).Column("diferenca_conta");
        }
    }
}