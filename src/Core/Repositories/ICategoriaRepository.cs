namespace Core.Repositories
{
    using Felice.Core.Model;
    using Models;

    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Categoria GetTransferencia();
        Categoria ObterAjusteDeSaldo();
    }
}