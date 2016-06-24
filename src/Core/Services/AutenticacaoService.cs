namespace Core.Services
{
    using Core.Models;

    public class AutenticacaoService
    {
        public void Autenticar(string senha)
        {
            if (senha.ToLower().Equals("admin") == false)
            {
                throw new RegraVioladaException("Senha inválida");
            }
        }
    }
}