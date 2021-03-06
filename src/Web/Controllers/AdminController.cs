﻿using System.Web.Mvc;

namespace Web.Controllers
{
    using Felice.Data;
    using Felice.Mvc;
    using Infra.Repositories;

    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Contas()
        {
            new ContaRepository().Seed();
            this.Success("Contas carregadas");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Categorias()
        {
            new CategoriaRepository().Seed();
            this.Success("Categorias carregadas");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Schema()
        {
            Database.MigrateToLastVersion();
            this.Success("Schema atualizado");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Limpar()
        {
            UnitOfWork.CurrentSession.CreateSQLQuery("delete from transacao").ExecuteUpdate();
            UnitOfWork.CurrentSession.CreateSQLQuery("delete from movimento").ExecuteUpdate();
            UnitOfWork.CurrentSession.CreateSQLQuery("update conta set saldo=0").ExecuteUpdate();
            this.Success("Base limpa com sucesso");
            return RedirectToAction("Index");
        }
    }
}
