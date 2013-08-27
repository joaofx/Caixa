namespace Core.Models
{
    using System.Collections.Generic;
    using Felice.Core.Model;

    public class Categoria : Entity
    {
        protected Categoria()
        {
        }

        public Categoria(long id)
        {
            this.Id = id;
        }

        public Categoria(long id, string nome)
            : this(id, nome, null)
        {
        }

        public Categoria(long id, string nome, List<Categoria> subCategorias)
        {
            this.Id = id;
            this.Nome = nome;
            this.SubCategorias = subCategorias ?? new List<Categoria>();

            foreach (var subCategoria in this.SubCategorias)
            {
                subCategoria.Parent = this;
            }
        }

        public virtual Categoria Parent
        {
            get;
            set;
        }

        public virtual string Nome
        {
            get;
            protected set;
        }

        public virtual List<Categoria> SubCategorias
        {
            get;
            protected set;
        }
    }
}