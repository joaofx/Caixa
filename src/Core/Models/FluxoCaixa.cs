namespace Core.Models
{
    using System;

    public class FluxoCaixa
    {
        public DateTime Data
        {
            get;
            set;
        }

        public decimal SaldoAnterior
        {
            get;
            set;
        }

        public decimal Entradas
        {
            get;
            set;
        }

        public decimal Saidas
        {
            get;
            set;
        }

        public decimal Saldo
        {
            get
            {
                return this.SaldoAnterior + this.Entradas + this.Saidas;
            }
        }
    }
}