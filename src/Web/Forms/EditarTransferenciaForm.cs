namespace Web.Forms
{
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using Core.Models;

    public class EditarTransferenciaForm
    {
        public ReadOnlyCollection<Conta> Contas
        {
            get;
            set;
        }

        public long Id
        {
            get;
            set;
        }

        [Display(Name = "Conta de Origem")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string ContaOrigemId
        {
            get;
            set;
        }

        [Display(Name = "Conta de Destino")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string ContaDestinoId
        {
            get;
            set;
        }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "{0} deve ser maior que 0")]
        public string Valor
        {
            get;
            set;
        }
    }
}