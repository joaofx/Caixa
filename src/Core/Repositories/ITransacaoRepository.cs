namespace Core.Repositories
{
    using System.Collections.ObjectModel;
    using Felice.Core.Model;
    using Models;

    public interface ITransacaoRepository : IRepository<Transacao>
    {
        ReadOnlyCollection<Transacao> AllByMovimento(Movimento movimento, int conta = 0);

        decimal SumByConta(Movimento movimento, long contaId);
    }
}