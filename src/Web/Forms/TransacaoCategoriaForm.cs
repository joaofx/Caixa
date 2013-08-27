namespace Web.Forms
{
    using System.Collections.ObjectModel;
    using Core.Models;

    public class TransacaoCategoriaForm : TransacaoContaForm
    {
        public string ContaId
        {
            get;
            set;
        }

        public ReadOnlyCollection<Categoria> Categorias
        {
            get;
            set;
        }

        public Conta Conta
        {
            get;
            set;
        }
    }
}