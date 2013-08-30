namespace Testes.Unitarios.Services
{
    using System;
    using Core.Models;
    using Core.Repositories;
    using Core.Services;
    using Felice.TestFramework;
    using NBehave.Spec.NUnit;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class AbreMovimentoServiceTest : MockedTest<AbreMovimentoService>
    {
        [Test]
        public void Deve_abrir_primeiro_movimento()
        {
            this.Get<IMovimentoRepository>()
                .GetAnterior()
                .Returns((Movimento) null);

            var movimento = this.ClassUnderTest.Abrir(DateTime.Parse("22/08/2013"));

            movimento.Data.ShouldEqual(DateTime.Parse("22/08/2013"));
            movimento.MovimentoAnterior.ShouldBeNull();

            this.Get<IMovimentoRepository>()
                .Received()
                .Save(movimento);
        }

        [Test]
        public void Deve_abrir_segundo_movimento()
        {
            var movimentoAnterior = new Movimento(DateTime.Parse("22/08/2013"), null);

            this.Get<IMovimentoRepository>()
                .GetAnterior()
                .Returns(movimentoAnterior);

            var movimento = this.ClassUnderTest.Abrir(DateTime.Parse("23/08/2013"));

            movimento.Data.ShouldEqual(DateTime.Parse("23/08/2013"));
            movimento.MovimentoAnterior.ShouldEqual(movimentoAnterior);

            this.Get<IMovimentoRepository>()
                .Received()
                .Save(movimento);
        }

        [Test]
        [ExpectedException(typeof(RegraVioladaException))]
        public void Deve_disparar_excecao_se_data_do_movimento_for_menor_que_a_anterior()
        {
            var movimentoAnterior = new Movimento(DateTime.Parse("22/08/2013"), null);

            this.Get<IMovimentoRepository>()
                .GetAnterior()
                .Returns(movimentoAnterior);

            this.ClassUnderTest.Abrir(DateTime.Parse("01/08/2013"));
        }
    }
}
