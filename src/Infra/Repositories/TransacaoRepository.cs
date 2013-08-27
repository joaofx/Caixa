namespace Infra.Repositories
{
    using System.Collections.ObjectModel;
    using Core.Models;
    using Core.Repositories;
    using Felice.Core;
    using Felice.Core.Data;

    public class TransacaoRepository : RepositoryBase<Transacao>, ITransacaoRepository
    {
        public ReadOnlyCollection<Transacao> AllByMovimento(Movimento movimento, int conta = 0)
        {
            var query = UnitOfWork.CurrentSession
                .QueryOver<Transacao>()
                .Where(x => x.Data == movimento.Data);

            if (conta != 0)
            {
                query.Where(x => x.Conta.Id == conta);
            }

            return query.OrderBy(x => x.Data).Desc
                .List()
                .AsReadOnly();
        }

        public decimal SumByConta(Movimento movimento, long contaId)
        {
            const string Hql = @"
select 
    sum(t.Valor) 
from 
    Transacao t
inner join t.Conta c
where 
    t.Data = :data and 
    c.Id = :contaId";

            return UnitOfWork.CurrentSession.CreateQuery(Hql)
                .SetParameter("data", movimento.Data)
                .SetParameter("contaId", contaId)
                .UniqueResult<decimal>();
        }
    }
}