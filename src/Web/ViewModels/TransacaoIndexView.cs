namespace Web.ViewModels
{
    using System.Collections.ObjectModel;
    using Core.Models;

    public class TransacaoIndexView
    {
        public Movimento MovimentoAtual
        {
            get;
            set;
        }

        public ReadOnlyCollection<Transacao> Transacoes
        {
            get;
            set;
        }

        public ReadOnlyCollection<Conta> Contas
        {
            get;
            set;
        }

        public string Conta
        {
            get;
            set;
        }
    }
}