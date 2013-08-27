namespace Web.Forms
{
    using System.ComponentModel.DataAnnotations;
    using Core.Models;

    public class FecharMovimentoForm
    {
        [Display(Name = "Saldo em Caixa")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "{0} deve ser um valor")]
        public string SaldoCaixa
        {
            get;
            set;
        }

        [Display(Name = "Saldo em Conta Corrente")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "{0} deve ser um valor")]
        public string SaldoConta
        {
            get;
            set;
        }

        public string MovimentoData
        {
            get;
            set;
        }

        public Fechamento Fechamento
        {
            get;
            set;
        }

        public bool Forcar
        {
            get;
            set;
        }
    }
}