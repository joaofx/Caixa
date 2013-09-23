namespace Infra.Repositories
{
    using System.Collections.ObjectModel;
    using Core.Models;
    using Core.Repositories;
    using Felice.Data;

    public class PendenciaRepository : RepositoryBase<Pendencia>, IPendenciaRepository
    {
        public ReadOnlyCollection<Pendencia> AllPendentes()
        {
            throw new System.NotImplementedException();
        }
    }
}