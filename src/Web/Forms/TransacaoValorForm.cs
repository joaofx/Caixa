namespace Web.Forms
{
    using Core.Models;

    public class TransacaoValorForm : TransacaoCategoriaForm
    {
        public string Valor
        {
            get;
            set;
        }

        public string Descricao
        {
            get;
            set;
        }

        public Categoria Categoria
        {
            get;
            set;
        }

        public string CategoriaId
        {
            get;
            set;
        }

        public long Id
        {
            get;
            set;
        }
    }
}