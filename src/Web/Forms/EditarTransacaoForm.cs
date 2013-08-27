namespace Web.Forms
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using Core.Models;
    using Felice.Core;
    using Infra.Repositories;

    public class EditarTransacaoForm
    {
        public ReadOnlyCollection<Conta> Contas
        {
            get;
            set;
        }

        public ReadOnlyCollection<Categoria> Categorias
        {
            get;
            set;
        }

        [Display(Name = "Conta")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string ContaId
        {
            get;
            set;
        }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string CategoriaId
        {
            get;
            set;
        }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Valor
        {
            get;
            set;
        }

        [Display(Name = "Tipo de transação")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public TipoTransacao Tipo
        {
            get;
            set;
        }

        [Display(Name = "Descrição")]
        public string Descricao
        {
            get;
            set;
        }

        public long Id
        {
            get;
            set;
        }

        public static EditarTransacaoForm FromModel(Transacao transacao)
        {
            return new EditarTransacaoForm
            {
                Id = transacao.Id,
                Contas = new ContaRepository().Todos(),
                Categorias = new CategoriaRepository().Hierarquia(),
                CategoriaId = transacao.Categoria.Id.ToString(),
                ContaId = transacao.Conta.Id.ToString(),
                Descricao = transacao.Descricao,
                Valor = Math.Abs(transacao.Valor).ToString()
            };
        }

        public Transacao ToModel(Movimento movimento)
        {
            return new Transacao(movimento)
            {
                Id = this.Id,
                Categoria = new Categoria(this.CategoriaId.ToLong()),
                Conta = new Conta(this.ContaId.ToLong()),
                Descricao = this.Descricao,
                Valor = this.Valor.ToDecimal2(),
                Tipo = this.Tipo
            };
        }
    }
}