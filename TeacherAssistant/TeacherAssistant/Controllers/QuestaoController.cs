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
    public class QuestaoController : BaseController
    {
        private DbEntidades db = new DbEntidades();

        //
        // GET: /Questao/

        public ActionResult Index()
        {
            return View(db.Questoes.ToList());
        }

        //
        // GET: /Questao/Create

        public ActionResult Create()
        {
            ViewBag.Tags = (from q in db.Questoes select q.Tag).Distinct().ToList();

            return View();
        }

        //
        // POST: /Questao/Create

        [HttpPost]
        public ActionResult Create(Questao questao)
        {
            if (ModelState.IsValid)
            {
                questao.Professor = db.Usuarios.Find(UsuarioLogado.User.Id);

                db.Questoes.Add(questao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tags = (from q in db.Questoes select q.Tag).Distinct().ToList();
            return View(questao);
        }

        //
        // GET: /Questao/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Questao questao = db.Questoes.Find(id);

            ViewBag.Tags = (from q in db.Questoes select q.Tag).Distinct().ToList();

            if (questao == null)
            {
                return HttpNotFound();
            }
            return View(questao);
        }

        //
        // POST: /Questao/Edit/5

        [HttpPost]
        public ActionResult Edit(Questao questao)
        {
            if (ModelState.IsValid)
            {
                Questao questaoDb = db.Questoes.Find(questao.Id);
                questaoDb.AlternativaA = questao.AlternativaA;
                questaoDb.AlternativaB = questao.AlternativaB;
                questaoDb.AlternativaC = questao.AlternativaC;
                questaoDb.AlternativaD = questao.AlternativaD;
                questaoDb.AlternativaE = questao.AlternativaE;
                questaoDb.Enunciado = questao.Enunciado;
                questaoDb.RespostaCorreta = questao.RespostaCorreta;
                questaoDb.Tag = questao.Tag;

                db.Entry(questaoDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tags = (from q in db.Questoes select q.Tag).Distinct().ToList();
            return View(questao);
        }

        //
        // GET: /Questao/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Questao questao = db.Questoes.Find(id);
            if (questao == null)
            {
                return HttpNotFound();
            }
            return View(questao);
        }

        //
        // POST: /Questao/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Questao questao = db.Questoes.Find(id);
            db.Questoes.Remove(questao);
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