namespace Core.Models
{
    public class Fechamento
    {
        public Fechamento(
            decimal saldoInformadoNoCaixa, 
            decimal saldoInformadoNaConta, 
            decimal somaDoCaixa, 
            decimal somaDaConta)
        {
            this.SaldoInformadoCaixa = saldoInformadoNoCaixa;
            this.SaldoInformadoConta = saldoInformadoNaConta;
            this.SomaDoCaixa = somaDoCaixa;
            this.SomaDaConta = somaDaConta;
            this.DiferencaNoCaixa = saldoInformadoNoCaixa - somaDoCaixa;
            this.DiferencaNaConta = saldoInformadoNaConta - somaDaConta;
            this.Batido = this.DiferencaNaConta == 0 && this.DiferencaNoCaixa == 0;
        }

        public decimal SomaDaConta
        {
            get;
            private set;
        }

        public decimal SomaDoCaixa
        {
            get;
            private set;
        }

        public bool Batido
        {
            get;
            private set;
        }

        public decimal DiferencaNaConta
        {
            get;
            private set;
        }

        public decimal DiferencaNoCaixa
        {
            get;
            private set;
        }

        public decimal SaldoInformadoCaixa
        {
            get;
            set;
        }

        public decimal SaldoInformadoConta
        {
            get;
            set;
        }
    }
}