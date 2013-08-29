namespace Core.Repositories
{
    using System.Collections.ObjectModel;
    using Felice.Core.Model;
    using Models;

    public interface IMovimentoRepository : IRepository<Movimento>
    {
        Movimento GetAnterior();

        Movimento GetAtual();
        ReadOnlyCollection<Movimento> Todos();
    }
}