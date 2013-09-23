namespace Core.Services
{
    using Models;

    public interface IRecebimentoService
    {
        Transacao Lancar(Conta conta, Categoria categoria, decimal valor, string descricao = "");
    }
}