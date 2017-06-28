using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherAssistant.Models;
using TeacherAssistant.ViewModels;
using TeacherAssistant.Util;

namespace TeacherAssistant.Controllers
{
    [CustomAuthorize]
    public class SelecionarTurmaController : BaseController
    {
        DbEntidades db = new DbEntidades();

        //
        // GET: /SelecionarTurma/

        public ActionResult Index()
        {
            IList<Turma> turmas;

            if (UsuarioLogado.User.Tipo == ((byte)TipoUsuario.Professor))
            {
                turmas = db.Turmas.Where(m => m.Professor.Id == UsuarioLogado.User.Id).ToList();
                if (turmas.Count <= 0)
                    return RedirectToAction("Index", "Turma");
            }
            else
            {
                turmas = db.Turmas.Where(m => m.Alunos.Where(al => al.Id == UsuarioLogado.User.Id).Any()).ToList();
                if (turmas.Count <= 0)
                {
                    ViewBag.MensagemValidacao = "Você não está vinculado a nenhuma turma no momento. Entre em contato com seu professor.";
                    return View();
                }
            }

            ViewBag.TurmaId = new SelectList(turmas, "Id", "Nome");

            return View();
        }

        [HttpPost]
        public ActionResult Index(SelecaoTurma selecao)
        {
            Turma turma = db.Turmas.Find(selecao.TurmaId);
            if (turma != null)
            {
                UsuarioLogado.Turma = turma;
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
