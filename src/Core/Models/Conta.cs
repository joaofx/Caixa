namespace Core.Models
{
    using Felice.Core.Model;

    public class Conta : Entity
    {
        public const long CaixaId = 1;
        public const long ContaCorrenteId = 2;

        protected Conta()
        {
        }

        public Conta(long id)
        {
            this.Id = id;
        }

        public Conta(long id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }

        public virtual string Nome
        {
            get;
            protected set;
        }

        public virtual decimal Saldo
        {
            get;
            set;
        }
    }
}