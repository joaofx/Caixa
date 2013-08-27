namespace Core.Models
{
    using System;
    using Felice.Core.Model;

    public class Transacao : Entity
    {
        public Transacao()
        {
        }

        public Transacao(Movimento movimento)
        {
            this.Data = movimento.Data;
            this.Movimento = movimento;
        }

        public virtual DateTime Data
        {
            get;
            protected set;
        }

        public virtual Movimento Movimento
        {
            get;
            protected set;
        }
        
        public virtual decimal Valor
        {
            get;
            set;
        }

        public virtual Conta Conta
        {
            get;
            set;
        }

        public virtual Categoria Categoria
        {
            get;
            set;
        }

        public virtual string Descricao
        {
            get;
            set;
        }

        public virtual TipoTransacao Tipo
        {
            get;
            set;
        }

        public virtual void FixarValor()
        {
            if (this.Tipo == TipoTransacao.Gasto && this.Valor > 0)
            {
                this.Valor = this.Valor * -1;
            }
            else if (this.Tipo == TipoTransacao.Recebimento && this.Valor < 0)
            {
                this.Valor = this.Valor * -1;
            }
        }
    }
}