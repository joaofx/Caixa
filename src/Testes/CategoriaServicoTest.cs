namespace Testes
{
    using Infra.Repositories;
    using NBehave.Spec.NUnit;
    using NUnit.Framework;

    [TestFixture]
    public class CategoriaServicoTest
    {
        [Test]
        public void Deve_retornar_em_arvore()
        {
            var arvore = new CategoriaRepository().Hierarquia();
            arvore[0].Nome.ShouldEqual("Receitas");
            arvore[0].SubCategorias[0].Nome.ShouldEqual("Vendas");
            arvore[1].Nome.ShouldEqual("Despesas");
            arvore[1].SubCategorias[0].Nome.ShouldEqual("Compras");
            arvore[1].SubCategorias[1].Nome.ShouldEqual("Aluguel");
        }

        [Test]
        public void Deve_retornar_em_lista()
        {
            var arvore = new CategoriaRepository().Todas();
            arvore[0].Nome.ShouldEqual("Receitas");
            arvore[1].Nome.ShouldEqual("Vendas");
            arvore[2].Nome.ShouldEqual("Despesas");
            arvore[3].Nome.ShouldEqual("Compras");
            arvore[4].Nome.ShouldEqual("Aluguel");
        }
    }
}