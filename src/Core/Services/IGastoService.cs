namespace Core.Services
{
    using Models;

    public interface IGastoService
    {
        Transacao Lancar(Conta conta, Categoria categoria, decimal valor, string descricao = "");
    }
}