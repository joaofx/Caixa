namespace Testes.Aceitacao
{
    using System;
    using Core.Models;
    using Core.Services;
    using Felice.Core;
    using Felice.Core.Data;
    using Felice.Core.Model;
    using Infra.Repositories;
    using NBehave.Spec.NUnit;
    using NUnit.Framework;

    [TestFixture]
    public class AcceptanceTest
    {
        private IUnitOfWork unitOfWork = Dependency.Resolve<IUnitOfWork>();
        private ContaRepository contaRepository = Dependency.Resolve<ContaRepository>();
        private CategoriaRepository categoriaRepository = Dependency.Resolve<CategoriaRepository>();
        private Conta caixa;
        private Conta itau;
        private FechaMovimentoService fechaMovimento;
        private TransferenciaService transferenciaService;
        private Categoria proLabore;
        private GastoService gastoService;
        private Categoria aluguel;
        private Categoria vendas;
        private RecebimentoService recebimentoService;
        private Categoria compras;
        private Categoria combustivel;
        private Categoria energia;
        private AbreMovimentoService abreMovimentoService;

        [SetUp]
        public void Setup()
        {
            this.unitOfWork = Dependency.Resolve<IUnitOfWork>();
            this.contaRepository = Dependency.Resolve<ContaRepository>();
            this.categoriaRepository = Dependency.Resolve<CategoriaRepository>();
            this.fechaMovimento = Dependency.Resolve<FechaMovimentoService>();
            this.transferenciaService = Dependency.Resolve<TransferenciaService>();
            this.gastoService = Dependency.Resolve<GastoService>();
            this.recebimentoService = Dependency.Resolve<RecebimentoService>();
            this.abreMovimentoService = Dependency.Resolve<AbreMovimentoService>();

            using (this.unitOfWork.Begin())
            {
                Dependency.Resolve<DatabaseCleaner>().Execute();
            }

            using (this.unitOfWork.Begin())
            {
                this.contaRepository.Seed();
                this.categoriaRepository.Seed();
            }

            using (this.unitOfWork.Begin())
            {
                this.caixa = this.contaRepository.ById(Conta.CaixaId);
                this.itau = this.contaRepository.ById(Conta.ContaCorrenteId);

                this.proLabore = this.categoriaRepository.ByNome("Pró-Labore");
                this.aluguel = this.categoriaRepository.ByNome("Aluguel");
                this.vendas = this.categoriaRepository.ByNome("Vendas 206");
                this.compras = this.categoriaRepository.ByNome("Compras");
                this.combustivel = this.categoriaRepository.ByNome("Combustível");
                this.energia = this.categoriaRepository.ByNome("Energia");
            }
        }

        [Test]
        public void Aceitacao()
        {
            UnitOfWork.Using(this.Lancamentos_de_01_do_10);
            UnitOfWork.Using(this.Saldos_de_01_do_10);

            UnitOfWork.Using(this.Lancamentos_de_02_do_10);
            UnitOfWork.Using(this.Saldos_de_02_do_10);

            UnitOfWork.Using(this.Lancamentos_de_03_do_10);
            UnitOfWork.Using(this.Saldos_de_03_do_10);

            UnitOfWork.Using(this.Lancamentos_de_04_do_10);
            UnitOfWork.Using(this.Saldos_de_04_do_10);
        }

        private void Lancamentos_de_01_do_10()
        {
            var mov = this.abreMovimentoService.Abrir(DateTime.Parse("01/10/2012"));

            // conta
            this.gastoService.Lancar(this.caixa, this.compras, 259.71m);
            this.gastoService.Lancar(this.caixa, this.combustivel, 30m);
            this.gastoService.Lancar(this.caixa, this.proLabore, 50m);
            this.recebimentoService.Lancar(this.caixa, this.vendas, 345.1m);
            
            // itau
            this.gastoService.Lancar(this.itau, this.energia, 49.80m);
            this.recebimentoService.Lancar(this.itau, this.vendas, 210.85m);

            this.fechaMovimento.Fechar(5.39m, 161.05m).Batido.ShouldBeTrue();
        }

        private void Saldos_de_01_do_10()
        {
            this.caixa = this.contaRepository.ById(Conta.CaixaId);
            this.itau = this.contaRepository.ById(Conta.ContaCorrenteId);

            this.caixa.Saldo.ShouldEqual(5.39m);
            this.itau.Saldo.ShouldEqual(161.05m);
        }

        private void Lancamentos_de_02_do_10()
        {
            var mov = this.abreMovimentoService.Abrir(DateTime.Parse("02/10/2012"));

            // caixa
            this.gastoService.Lancar(this.caixa, this.combustivel, 15m);
            this.gastoService.Lancar(this.caixa, this.proLabore, 50m);
            this.recebimentoService.Lancar(this.caixa, this.vendas, 557.8m);

            // itau
            this.gastoService.Lancar(this.itau, this.compras, 120.7m);
            this.recebimentoService.Lancar(this.itau, this.vendas, 488.9m);

            this.fechaMovimento.Fechar(498.19m, 529.25m).Batido.ShouldBeTrue();
        }

        private void Saldos_de_02_do_10()
        {
            this.caixa = this.contaRepository.ById(Conta.CaixaId);
            this.itau = this.contaRepository.ById(Conta.ContaCorrenteId);

            this.caixa.Saldo.ShouldEqual(498.19m);
            this.itau.Saldo.ShouldEqual(529.25m);
        }
        
        private void Lancamentos_de_03_do_10()
        {
            var mov = this.abreMovimentoService.Abrir(DateTime.Parse("03/10/2012"));

            // caixa
            this.gastoService.Lancar(this.caixa, this.compras, 266.2m);
            this.gastoService.Lancar(this.caixa, this.combustivel, 10m);
            this.gastoService.Lancar(this.caixa, this.proLabore, 50m);
            this.recebimentoService.Lancar(this.caixa, this.vendas, 333.1m);
                
            // itau
            this.gastoService.Lancar(this.itau, this.aluguel, 400m);
            this.recebimentoService.Lancar(this.itau, this.vendas, 300.15m);

            var fechamento = this.fechaMovimento.Fechar(550m, 440m);

            fechamento.Batido.ShouldBeFalse();
            fechamento.DiferencaNoCaixa.ShouldEqual(44.91m);
            fechamento.DiferencaNaConta.ShouldEqual(10.6m);

            this.fechaMovimento.FecharComDiferenca(550m, 440m);

            mov.Status.ShouldEqual(MovimentoStatus.FechadoComDiferenca);
        }

        private void Saldos_de_03_do_10()
        {
            this.caixa = this.contaRepository.ById(Conta.CaixaId);
            this.itau = this.contaRepository.ById(Conta.ContaCorrenteId);

            this.caixa.Saldo.ShouldEqual(550m);
            this.itau.Saldo.ShouldEqual(440m);
        }


        private void Lancamentos_de_04_do_10()
        {
            var mov = this.abreMovimentoService.Abrir(DateTime.Parse("04/10/2012"));

            // caixa
            this.gastoService.Lancar(this.caixa, this.proLabore, 50m);
            this.gastoService.Lancar(this.caixa, this.aluguel, 400m);
            this.recebimentoService.Lancar(this.caixa, this.vendas, 250.33m);

            // itau
            this.transferenciaService.Transferir(this.itau.Id, this.caixa.Id, 200);
            this.recebimentoService.Lancar(this.itau, this.vendas, 130.55m);

            var fechamento = this.fechaMovimento.Fechar(570m, 370.55m);

            fechamento.Batido.ShouldBeFalse();
            fechamento.DiferencaNoCaixa.ShouldEqual(19.67m);
            fechamento.DiferencaNaConta.ShouldEqual(0m);

            this.fechaMovimento.FecharComDiferenca(570m, 370.55m);

            mov.Status.ShouldEqual(MovimentoStatus.FechadoComDiferenca);
        }

        private void Saldos_de_04_do_10()
        {
            this.caixa = this.contaRepository.ById(Conta.CaixaId);
            this.itau = this.contaRepository.ById(Conta.ContaCorrenteId);

            this.caixa.Saldo.ShouldEqual(570m);
            this.itau.Saldo.ShouldEqual(370.55m);
        }
    }
}