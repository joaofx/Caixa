namespace Testes
{
    using Core.Models;
    using Felice.Core.Data;
    using Felice.TestFramework;

    public class DatabaseCleaner : DaoBase, IDatabaseCleaner
    {
        public void Execute()
        {
            this.Session.DeleteAll<Transacao>();
            this.Session.DeleteAll<Movimento>();
        }
    }
}
