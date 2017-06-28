using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherAssistant.Models;
using TeacherAssistant.Util;

namespace TeacherAssistant.Controllers
{
    [CustomAuthorize(TipoUsuario.Professor)]
    public class LicaoController : BaseController
    {
        private DbEntidades db = new DbEntidades();

        //
        // GET: /Licao/

        public ActionResult Index()
        {
            return View(db.Licoes.Where(m => m.Professor.Id == UsuarioLogado.User.Id).ToList());
        }

        //
        // GET: /Licao/Create

        public ActionResult Create()
        {
            ViewBag.Turma = new SelectList(db.Turmas.Where(t => t.Professor.Id == UsuarioLogado.User.Id).ToList(), "Id", "Nome");
            ViewBag.Questionarios = new SelectList(db.Questionarios.Where(m => m.Professor.Id == UsuarioLogado.User.Id).ToList(), "Id", "Titulo");

            return View();
        }

        //
        // POST: /Licao/Create

        [HttpPost]
        public ActionResult Create(Licao licao)
        {
            if (ModelState["Questionario.Id"].Errors.Count > 0)
                ModelState.Remove("Questionario.Id");

            if (ModelState.IsValid)
            {
                licao.Turma = db.Turmas.Find(UsuarioLogado.Turma.Id);
                licao.Professor = db.Usuarios.Find(UsuarioLogado.User.Id);
                licao.Forum = Forum.Create();
                if (licao.Questionario != null)
                    licao.Questionario = db.Questionarios.Find(licao.Questionario.Id);
                db.Licoes.Add(licao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Turma = new SelectList(db.Turmas.Where(t => t.Professor.Id == UsuarioLogado.User.Id).ToList(), "Id", "Nome");
            ViewBag.Questionarios = new SelectList(db.Questionarios.Where(m => m.Professor.Id == UsuarioLogado.User.Id).ToList(), "Id", "Titulo");
            return View(licao);
        }

        //
        // GET: /Licao/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Licao licao = db.Licoes.Find(id);
            if (licao == null)
            {
                return HttpNotFound();
            }

            ViewBag.Questionarios = new SelectList(db.Questionarios.Where(m => m.Professor.Id == UsuarioLogado.User.Id).ToList(), "Id", "Titulo");
            return View(licao);
        }

        //
        // POST: /Licao/Edit/5

        [HttpPost]
        public ActionResult Edit(Licao licao)
        {
            if (ModelState["Questionario.Id"].Errors.Count > 0)
                ModelState.Remove("Questionario.Id");

            if (ModelState.IsValid)
            {
                Licao licaoDb = db.Licoes.Find(licao.Id);
                licaoDb.Titulo = licao.Titulo;
                licaoDb.Descricao = licao.Descricao;
                if (licao.Questionario != null)
                    licaoDb.Questionario = db.Questionarios.Find(licao.Questionario.Id);
                db.Entry(licaoDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Questionarios = new SelectList(db.Questionarios.Where(m => m.Professor.Id == UsuarioLogado.User.Id).ToList(), "Id", "Titulo");
            return View(licao);
        }

        //
        // GET: /Licao/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Licao licao = db.Licoes.Find(id);
            if (licao == null)
            {
                return HttpNotFound();
            }
            return View(licao);
        }

        //
        // POST: /Licao/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Licao licao = db.Licoes.Find(id);
            db.Licoes.Remove(licao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}