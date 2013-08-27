namespace Infra.Repositories
{
    using Core.Models;
    using Core.Repositories;
    using Felice.Core.Data;

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
    }
}