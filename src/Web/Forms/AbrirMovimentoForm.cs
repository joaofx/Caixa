﻿namespace Web.Forms
{
    using System.ComponentModel.DataAnnotations;
    using Core.Models;
    using Felice.Core.Mvc;

    public class AbrirMovimentoForm
    {
        [Display(Name = "Data do Novo Movimento")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [DateIsValid(ErrorMessage = "{0} deve ser uma data válida")]
        public string DataNovoMovimento
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