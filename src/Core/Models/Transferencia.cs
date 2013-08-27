namespace Core.Models
{
    public class Transferencia
    {
        public Transferencia(Transacao transacaoDebito, Transacao transacaoCredito)
        {
            this.TransacaoDebito = transacaoDebito;
            this.TransacaoCredito = transacaoCredito;
        }

        public Transacao TransacaoDebito
        {
            get;
            private set;
        }

        public Transacao TransacaoCredito
        {
            get;
            private set;
        }
    }
}