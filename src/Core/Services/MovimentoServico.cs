namespace Core.Services
{
    using System.Collections.ObjectModel;
    using Felice.Core;
    using Felice.Data;
    using Models;

    public class MovimentoServico
    {
        public ReadOnlyCollection<Movimento> Todos()
        {
            return UnitOfWork.CurrentSession.QueryOver<Movimento>()
                .OrderBy(x => x.Data)
                .Desc
                .List()
                .AsReadOnly();
        }
    }
}