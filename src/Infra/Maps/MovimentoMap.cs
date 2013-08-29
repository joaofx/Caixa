namespace Infra.Maps
{
    using Core.Models;
    using FluentNHibernate.Mapping;

    public class MovimentoMap : ClassMap<Movimento>
    {
        public MovimentoMap()
        {
            this.Table("movimento");
            this.Id(x => x.Id).GeneratedBy.Native().Column("id");
            this.Map(x => x.Data).Column("data");
            this.Map(x => x.SaldoFinalCaixa).Column("saldo_caixa");
            this.Map(x => x.SaldoFinalConta).Column("saldo_conta");
            this.Map(x => x.Status).Column("status");
            this.Map(x => x.DiferencaNoCaixa).Column("diferenca_caixa");
            this.Map(x => x.DiferencaNaConta).Column("diferenca_conta");

            this.References(x => x.MovimentoAnterior).Column("movimento_anterior_id");
        }
    }
}