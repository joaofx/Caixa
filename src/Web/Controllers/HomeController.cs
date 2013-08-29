using System.Web.Mvc;

namespace Web.Controllers
{
    using Core.Services;
    using Felice.Data;
    using Felice.Mvc;
    using Helpers;
    using Infra.Repositories;

    public class HomeController : Controller
    {
        private readonly MovimentoRepository movimentoRepository;

        public HomeController(MovimentoRepository movimentoRepository)
        {
            this.movimentoRepository = movimentoRepository;
        }

        [ComMovimento]
        public ActionResult Index()
        {
            return View();
            ////return View(new HomeIndexView()
            ////{
            ////    MovimentoAtual = this.movimentoServico.ObterAtual(),
            ////    MovimentoAnterior = this.movimentoServico.ObterAnterior()
            ////});
        }

        public ActionResult Schema()
        {
            Database.MigrateToLastVersion();

            UnitOfWork.CurrentSession.CreateSQLQuery("delete from conta").ExecuteUpdate();
            UnitOfWork.CurrentSession.CreateSQLQuery("delete from categoria").ExecuteUpdate();

            foreach (var conta in new ContaRepository().Todos())
            {
                UnitOfWork.CurrentSession
                    .CreateSQLQuery("insert into conta (id, nome) values (:id, :nome)")
                    .SetParameter("id", conta.Id)
                    .SetParameter("nome", conta.Nome)
                    .ExecuteUpdate();
            }

            foreach (var categoria in new CategoriaRepository().Hierarquia())
            {
                UnitOfWork.CurrentSession
                    .CreateSQLQuery("insert into categoria (id, nome) values (:id, :nome)")
                    .SetParameter("id", categoria.Id)
                    .SetParameter("nome", categoria.Nome)
                    .ExecuteUpdate();
            }


            this.Success("Schema atualizado");
            return RedirectToAction("Index");
        }
    }
}
