namespace Core.Services
{
    using Core.Models;

    public class AutenticacaoService
    {
        public void Autenticar(string senha)
        {
            if (senha.ToLower().Equals("foca") == false)
            {
                throw new RegraVioladaException("Senha inv�lida");
            }
        }
    }
}