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
    public class TurmaController : BaseController
    {
        private DbEntidades db = new DbEntidades();

        //
        // GET: /Turma/

        public ActionResult Index()
        {
            return View(db.Turmas.Where(m => m.Professor.Id == UsuarioLogado.User.Id).ToList());
        }

        //
        // GET: /Turma/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Turma/Create

        [HttpPost]
        public ActionResult Create(Turma turma)
        {
            if (ModelState.IsValid)
            {
                turma.Professor = db.Usuarios.Find(UsuarioLogado.User.Id);
                db.Turmas.Add(turma);
                //db.Turmas.Add(turma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(turma);
        }

        //
        // GET: /Turma/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Turma turma = db.Turmas.Find(id);
            if (turma == null)
            {
                return HttpNotFound();
            }
            return View(turma);
        }

        //
        // POST: /Turma/Edit/5

        [HttpPost]
        public ActionResult Edit(Turma turma)
        {
            if (ModelState.IsValid)
            {
                turma.Professor = db.Usuarios.Find(UsuarioLogado.User.Id);
                db.Entry(turma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(turma);
        }

        //
        // GET: /Turma/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Turma turma = db.Turmas.Find(id);
            if (turma == null)
            {
                return HttpNotFound();
            }
            return View(turma);
        }

        //
        // POST: /Turma/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Turma turma = db.Turmas.Find(id);
            db.Turmas.Remove(turma);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Alunos(long id)
        {
            var alunos = db.Usuarios.Where(u => u.Tipo == ((byte)TipoUsuario.Aluno)).ToList();
            var turma = db.Turmas.Find(id);

            List<SelectListItem> itens = new List<SelectListItem>();
            foreach (var aluno in alunos)
            {
                bool selected = turma.Alunos.Contains(aluno);

                itens.Add(new SelectListItem()
                {
                    Text = aluno.Nome,
                    Value = aluno.Id.ToString(),
                    Selected = selected
                });
            }

            ViewBag.Alunos = itens;

            return View(turma);
        }

        [HttpPost]
        public ActionResult Alunos(Turma turma)
        {
            Turma turmaDb = db.Turmas.Find(turma.Id);
            turmaDb.Alunos = new List<Usuario>();

            foreach (Usuario aluno in turma.Alunos)
                turmaDb.Alunos.Add(db.Usuarios.Find(aluno.Id));

            db.Entry(turmaDb).State = EntityState.Modified;
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