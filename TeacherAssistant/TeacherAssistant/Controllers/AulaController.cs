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
    public class AulaController : BaseController
    {
        private DbEntidades db = new DbEntidades();

        //
        // GET: /Aula/

        public ActionResult Index()
        {
            return View(db.Aulas.Where(m => m.Licao.Professor.Id == UsuarioLogado.User.Id).ToList());
        }

        //
        // GET: /Aula/Create

        public ActionResult Create()
        {
            var licoes = db.Licoes.Where(m => m.Turma.Id == UsuarioLogado.Turma.Id).ToList();
            if (licoes.Count <= 0)
                throw new ApplicationException("Cadastre antes uma lição para depois cadastrar uma aula.");

            ViewBag.Questionarios = new SelectList(db.Questionarios.Where(m => m.Professor.Id == UsuarioLogado.User.Id).ToList(), "Id", "Titulo");
            ViewBag.Licoes = new SelectList(licoes, "Id", "Titulo");

            return View();
        }

        //
        // POST: /Aula/Create

        [HttpPost]
        public ActionResult Create(Aula aula)
        {
            if (ModelState["Questionario.Id"].Errors.Count > 0)
                ModelState.Remove("Questionario.Id");

            if (ModelState.IsValid)
            {
                aula.Licao = db.Licoes.Find(aula.Licao.Id);
                aula.Forum = Forum.Create();
                if (aula.Questionario != null)
                    aula.Questionario = db.Questionarios.Find(aula.Questionario.Id);

                db.Aulas.Add(aula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Questionarios = new SelectList(db.Questionarios.Where(m => m.Professor.Id == UsuarioLogado.User.Id).ToList(), "Id", "Titulo");
            ViewBag.Licoes = new SelectList(db.Licoes.Where(m => m.Turma.Id == UsuarioLogado.Turma.Id).ToList(), "Id", "Titulo");
            return View(aula);
        }

        //
        // GET: /Aula/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Aula aula = db.Aulas.Find(id);

            ViewBag.Questionarios = new SelectList(db.Questionarios.Where(m => m.Professor.Id == UsuarioLogado.User.Id).ToList(), "Id", "Titulo");

            if (aula == null)
            {
                return HttpNotFound();
            }
            return View(aula);
        }

        //
        // POST: /Aula/Edit/5

        [HttpPost]
        public ActionResult Edit(Aula aula)
        {
            if (ModelState["Questionario.Id"].Errors.Count > 0)
                ModelState.Remove("Questionario.Id");

            if (ModelState.IsValid)
            {
                Aula aulaDb = db.Aulas.Find(aula.Id);
                aulaDb.Titulo = aula.Titulo;
                aulaDb.Descricao = aula.Descricao;
                if (aula.Questionario != null)
                    aulaDb.Questionario = db.Questionarios.Find(aula.Questionario.Id);
                aulaDb.DataInicioQuestionario = aula.DataInicioQuestionario;
                aulaDb.DataLimiteQuestionario = aula.DataLimiteQuestionario;

                db.Entry(aulaDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Questionarios = new SelectList(db.Questionarios.Where(m => m.Professor.Id == UsuarioLogado.User.Id).ToList(), "Id", "Titulo");
            return View(aula);
        }

        //
        // GET: /Aula/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Aula aula = db.Aulas.Find(id);
            if (aula == null)
            {
                return HttpNotFound();
            }
            return View(aula);
        }

        //
        // POST: /Aula/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Aula aula = db.Aulas.Find(id);
            db.Aulas.Remove(aula);
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