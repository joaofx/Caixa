namespace Testes
{
    using Core.Models;
    using Felice.Data;
    using Felice.TestFramework;

    public class DatabaseCleaner : DaoBase, IDatabaseCleaner
    {
        public void Execute()
        {
            this.Session.DeleteAll<Transacao>();
            this.Session.DeleteAll<Movimento>();
            this.Session.DeleteAll<Categoria>();
            this.Session.DeleteAll<Conta>();
        }
    }
}
