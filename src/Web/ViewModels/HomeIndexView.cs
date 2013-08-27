namespace Web.ViewModels
{
    using Core.Models;

    public class HomeIndexView
    {
        public Movimento MovimentoAtual
        {
            get;
            set;
        }

        public Movimento MovimentoAnterior
        {
            get;
            set;
        }
    }
}