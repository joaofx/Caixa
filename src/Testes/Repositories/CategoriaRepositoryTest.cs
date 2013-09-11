namespace IntegrationTests.Repositories
{
    using Core.Models;
    using Felice.TestFramework;
    using Infra.Repositories;
    using NBehave.Spec.NUnit;
    using NUnit.Framework;

    [TestFixture]
    public class CategoriaRepositoryTest : RepositoryTest<Categoria, CategoriaRepository>
    {
        [Test]
        public void Deve_retornar_em_arvore()
        {
            var arvore = new CategoriaRepository().Hierarquia();
            arvore[0].Nome.ShouldEqual("Receitas");
            arvore[0].SubCategorias[0].Nome.ShouldEqual("Vendas 206");
            arvore[1].Nome.ShouldEqual("Loja");
            arvore[1].SubCategorias[0].Nome.ShouldEqual("Água");
            arvore[1].SubCategorias[1].Nome.ShouldEqual("Aluguel");
        }

        [Test]
        public void Deve_retornar_em_lista()
        {
            var arvore = new CategoriaRepository().Todas();
            arvore[0].Nome.ShouldEqual("Receitas");
            arvore[1].Nome.ShouldEqual("Vendas 206");
            arvore[2].Nome.ShouldEqual("Vendas Funarte");
            arvore[3].Nome.ShouldEqual("Encomendas");
            arvore[4].Nome.ShouldEqual("Loja");
        }

        public override Categoria CreateEntity()
        {
            return new Categoria();
        }
    }
}