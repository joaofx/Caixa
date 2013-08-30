namespace Infra.Repositories
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Core.Models;
    using Core.Repositories;
    using Felice.Data;

    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {
        private readonly List<Categoria> categorias;

        public CategoriaRepository()
        {
            var i = 1;

            this.categorias = new List<Categoria>()
            {
                new Categoria(i++, "Receitas", new List<Categoria>()
                {
                    new Categoria(i++, "Vendas 206"),
                    new Categoria(i++, "Vendas Funarte"),
                    new Categoria(i++, "Encomendas")
                }),
                new Categoria(i++, "Loja", new List<Categoria>()
                {
                    new Categoria(i++, "Água"),
                    new Categoria(i++, "Aluguel"),
                    new Categoria(i++, "Condominio"),
                    new Categoria(i++, "Energia"),
                    new Categoria(i++, "Manutenção da Loja"),
                    new Categoria(i++, "Telefone"),
                    new Categoria(i++, "Gas"),
                }),
                new Categoria(i++, "Operacional", new List<Categoria>()
                {
                    new Categoria(i++, "Combustível"),
                    new Categoria(i++, "Compras"),
                    new Categoria(i++, "Músicos"),
                    new Categoria(i++, "Materiais"),
                }),
                new Categoria(i++, "Pessoal", new List<Categoria>()
                {
                    new Categoria(i++, "Salário Tereza"),
                    new Categoria(i++, "Transporte"),
                    new Categoria(i++, "Pró-Labore"),
                    new Categoria(i++, "Free-Lance"),
                }),
                new Categoria(i++, "Impostos", new List<Categoria>()
                {
                    new Categoria(i++, "FGTS"),
                    new Categoria(i++, "GPS"),
                    new Categoria(i++, "IPTU"),
                    new Categoria(i++, "Simples Nacional"),
                }),
                new Categoria(i++, "Empréstimos", new List<Categoria>()
                {
                    new Categoria(i++, "Empréstimo Adega"),
                    new Categoria(i++, "Empréstimo João"),
                    new Categoria(i++, "Empréstimo Clara")
                }),
                new Categoria(i++, "Transferência"),
                new Categoria(i++, "Ajuste de Saldo"),
            };
        }

        public ReadOnlyCollection<Categoria> Todas()
        {
            var todas = new List<Categoria>();

            foreach (var pai in this.categorias)
            {
                todas.Add(pai);

                foreach (var categoria in pai.SubCategorias)
                {
                    todas.Add(categoria);
                }
            }

            return todas.AsReadOnly();
        }

        public ReadOnlyCollection<Categoria> TodasEmArvore()
        {
            var todas = new List<Categoria>();

            foreach (var pai in this.categorias)
            {
                todas.Add(pai);

                foreach (var categoria in pai.SubCategorias)
                {
                    todas.Add(categoria);
                }
            }

            return todas.AsReadOnly();
        }

        public ReadOnlyCollection<Categoria> Hierarquia()
        {
            return this.categorias.AsReadOnly();
        }

        public Categoria PorId(long id)
        {
            return UnitOfWork.CurrentSession.Get<Categoria>(id);
        }

        public Categoria GetTransferencia()
        {
            return this.categorias.SingleOrDefault(x => x.Nome == "Transferência");
        }

        public Categoria ObterAjusteDeSaldo()
        {
            return this.categorias.SingleOrDefault(x => x.Nome == "Ajuste de Saldo");
        }

        public void Seed()
        {
            UnitOfWork.CurrentSession.CreateSQLQuery("delete from categoria").ExecuteUpdate();

            foreach (var categoria in new CategoriaRepository().Todas())
            {
                UnitOfWork.CurrentSession
                    .CreateSQLQuery("insert into categoria (id, nome, categoria_id) values (:id, :nome, :categoria_id)")
                    .SetParameter("id", categoria.Id)
                    .SetParameter("nome", categoria.Nome)
                    .SetParameter("categoria_id", categoria.Parent != null ? (int?)categoria.Parent.Id : null)
                    .ExecuteUpdate();
            }
        }

        public Categoria ByNome(string nome)
        {
            return this.Todas().FirstOrDefault(x => x.Nome == nome);
        }
    }
}