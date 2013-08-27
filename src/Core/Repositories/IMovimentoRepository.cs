namespace Core.Repositories
{
    using Felice.Core.Model;
    using Models;

    public interface IMovimentoRepository : IRepository<Movimento>
    {
        Movimento GetAnterior();

        Movimento GetAtual();
    }
}