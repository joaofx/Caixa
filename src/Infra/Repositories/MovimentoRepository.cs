namespace Infra.Repositories
{
    using System.Collections.ObjectModel;
    using Core.Models;
    using Core.Repositories;
    using Felice.Core;
    using Felice.Data;

    public class MovimentoRepository : RepositoryBase<Movimento>, IMovimentoRepository
    {
        public Movimento GetAnterior()
        {
            return this.Session.QueryOver<Movimento>()
                .Where(x => x.Status == MovimentoStatus.Fechado)
                .OrderBy(x => x.Data).Desc
                .Take(1)
                .SingleOrDefault();
        }

        public Movimento GetAtual()
        {
            return UnitOfWork.CurrentSession
                .QueryOver<Movimento>()
                .Where(x => x.Status == MovimentoStatus.Aberto)
                .OrderBy(x => x.Data).Desc
                .SingleOrDefault();
        }

        public ReadOnlyCollection<Movimento> Todos()
        {
            return UnitOfWork.CurrentSession.QueryOver<Movimento>()
                .OrderBy(x => x.Data)
                .Desc
                .List()
                .AsReadOnly();
        }
    }
}