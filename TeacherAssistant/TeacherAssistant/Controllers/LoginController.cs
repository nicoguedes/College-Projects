using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherAssistant.Models;
using TeacherAssistant.Util;

namespace TeacherAssistant.Controllers
{
    public class LoginController : BaseController
    {
        DbEntidades db = new DbEntidades();

        //
        // GET: /Login/

        public ActionResult Index()
        {
            if (UsuarioLogado.User != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            string senhaCriptografada = Criptografia.CriptografarMd5(usuario.Senha);

            var lst = db.Usuarios.Where(u => u.Login == usuario.Login && u.Senha == senhaCriptografada);

            if (!lst.Any())
            {
                ModelState.AddModelError("Invalido", "Usuário ou senha inválidos.");
                return View(usuario);
            }
            else
            {
                UsuarioLogado.User = lst.First();
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Sair()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Login");
        }
    }
}
