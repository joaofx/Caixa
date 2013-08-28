namespace Infra.Repositories
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Core.Models;
    using Core.Repositories;
    using Felice.Core.Data;

    public class ContaRepository : RepositoryBase<Conta>, IContaRepository
    {
        public ReadOnlyCollection<Conta> Todos()
        {
            return new List<Conta>
            {
                new Conta(Conta.CaixaId, "Caixa"),
                new Conta(Conta.ContaCorrenteId, "Conta Corrente")
            }.AsReadOnly();
        }

        public void Seed()
        {
            this.Session.CreateSQLQuery("delete from conta").ExecuteUpdate();

            foreach (var conta in new ContaRepository().Todos())
            {
                this.Session
                    .CreateSQLQuery("insert into conta (id, nome) values (:id, :nome)")
                    .SetParameter("id", conta.Id)
                    .SetParameter("nome", conta.Nome)
                    .ExecuteUpdate();
            }
        }
    }
}