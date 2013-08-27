namespace Web.Forms
{
    using System.ComponentModel.DataAnnotations;

    public class TrocarSenhaForm
    {
        [Display(Name = "Senha Atual")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string SenhaAtual
        {
            get;
            set;
        }

        [Display(Name = "Nova Senha")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string NovaSenha
        {
            get;
            set;
        }

        [Display(Name = "Confirmação da Nova Senha")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string ConfirmacaoNovaSenha
        {
            get;
            set;
        }
    }
}