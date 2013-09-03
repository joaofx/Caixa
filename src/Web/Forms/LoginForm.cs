namespace Web.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class LoginForm
    {
        ////[DisplayName("Usuário")]
        ////[Required(ErrorMessage = "{0} é obrigatório")]
        ////public string Usuario
        ////{
        ////    get;
        ////    set;
        ////}

        [DisplayName("Senha")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Senha
        {
            get;
            set;
        }
    }
}