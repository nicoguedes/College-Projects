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
    public class QuestionarioController : BaseController
    {
        private DbEntidades db = new DbEntidades();

        //
        // GET: /Questionario/

        public ActionResult Index()
        {
            return View(db.Questionarios.Where(q => q.Professor.Id == UsuarioLogado.User.Id).ToList());
        }

        //
        // GET: /Questionario/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Questionario/Create

        [HttpPost]
        public ActionResult Create(Questionario questionario)
        {
            if (ModelState.IsValid)
            {
                questionario.Professor = db.Usuarios.Find(UsuarioLogado.User.Id);
                db.Questionarios.Add(questionario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionario);
        }

        //
        // GET: /Questionario/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Questionario questionario = db.Questionarios.Find(id);
            if (questionario == null)
            {
                return HttpNotFound();
            }
            return View(questionario);
        }

        //
        // POST: /Questionario/Edit/5

        [HttpPost]
        public ActionResult Edit(Questionario questionario)
        {
            if (ModelState.IsValid)
            {
                Questionario questionarioDb = db.Questionarios.Find(questionario.Id);

                questionarioDb.ErrosPermitidos = questionario.ErrosPermitidos;
                questionarioDb.Titulo = questionario.Titulo;
                questionarioDb.QuantidadeQuestoes = questionario.QuantidadeQuestoes;

                db.Entry(questionarioDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questionario);
        }

        //
        // GET: /Questionario/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Questionario questionario = db.Questionarios.Find(id);
            if (questionario == null)
            {
                return HttpNotFound();
            }
            return View(questionario);
        }

        //
        // POST: /Questionario/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Questionario questionario = db.Questionarios.Find(id);
            db.Questionarios.Remove(questionario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Questoes(long id)
        {
            Questionario questionario = db.Questionarios.Find(id);

            PreencherViewBagQuestoes(questionario);

            return View(questionario);
        }

        [HttpPost]
        public ActionResult Questoes(Questionario questionario)
        {
            if (ModelState.IsValid)
            {
                Questionario questionarioDb = db.Questionarios.Find(questionario.Id);
                questionarioDb.Questoes = new List<Questao>();

                foreach (Questao questao in questionario.Questoes)
                    questionarioDb.Questoes.Add(db.Questoes.Find(questao.Id));

                db.Entry(questionarioDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                PreencherViewBagQuestoes(questionario);

                return View(questionario);
            }
        }

        private void PreencherViewBagQuestoes(Questionario questionario)
        {
            List<Questao> questoes = db.Questoes.Where(q => q.Professor.Id == UsuarioLogado.User.Id).ToList();

            foreach (var questao in questoes)
                questao.Selected = questionario.Questoes.Contains(questao);

            ViewBag.Questoes = questoes.GroupBy(q => q.Tag).ToList();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}