namespace Core.Models
{
    using System;
    using Felice.Core;
    using Felice.Core.Model;

    public class Movimento : Entity
    {
        protected Movimento()
        {
        }

        public Movimento(DateTime data) : this(data, null)
        {
        }

        public Movimento(DateTime data, Movimento movimentoAnterior)
        {
            if (movimentoAnterior != null && data <= movimentoAnterior.Data)
            {
                throw new RegraVioladaException(
                    "Data do novo movimento ({0}) deve ser maior que a do movimento anterior ({1})",
                    data.Show(),
                    movimentoAnterior.Data.Show());
            }

            this.Data = data;
            this.MovimentoAnterior = movimentoAnterior;
            this.Status = MovimentoStatus.Aberto;
        }

        public virtual MovimentoStatus Status
        {
            get;
            protected set;
        }

        public virtual DateTime Data
        {
            get;
            protected set;
        }

        public virtual decimal SaldoFinalCaixa
        {
            get;
            protected set;
        }

        public virtual decimal SaldoFinalConta
        {
            get;
            protected set;
        }

        public virtual Movimento MovimentoAnterior
        {
            get;
            protected set;
        }

        public virtual decimal DiferencaNoCaixa
        {
            get;
            protected set;
        }

        public virtual decimal DiferencaNaConta
        {
            get;
            protected set;
        }

        public virtual void Fechar(Fechamento fechamento)
        {
            this.SaldoFinalCaixa = fechamento.SomaDoCaixa;
            this.SaldoFinalConta = fechamento.SomaDaConta;
            this.Status = MovimentoStatus.Fechado;
        }

        public virtual void FecharComDiferenca(Fechamento fechamento)
        {
            this.SaldoFinalCaixa = fechamento.SomaDoCaixa + fechamento.DiferencaNoCaixa;
            this.SaldoFinalConta = fechamento.SomaDaConta + fechamento.DiferencaNaConta;
            this.DiferencaNoCaixa = fechamento.DiferencaNoCaixa;
            this.DiferencaNaConta = fechamento.DiferencaNaConta;
            this.Status = MovimentoStatus.FechadoComDiferenca;
        }
    }
}