using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherAssistant.Util;
using TeacherAssistant.ViewModels;

namespace TeacherAssistant.Controllers
{
    [CustomAuthorize]
    public class HomeController : BaseController
    {
        DbEntidades db = new DbEntidades();

        public ActionResult Index()
        {

            DadosHomePage dados = new DadosHomePage() { 
                Licoes = db.Licoes.Where(m => m.Turma.Id == UsuarioLogado.Turma.Id).ToList(),
                MensagensNaoLidas = db.MensagensUsuario.Where(m => m.Destinatario.Id == UsuarioLogado.User.Id && !m.Lida).ToList()
            };

            return View(dados);
        }
    }
}
