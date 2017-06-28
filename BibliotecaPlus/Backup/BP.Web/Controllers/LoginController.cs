using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Integracao.Repository;
using BP.Web.Models;
using Dominio;
using System.Net.Mail;
using System.Net;

namespace BP.Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            if (UsuarioLogado.IsUsuarioLogado)
                return RedirectToAction("Index", "Home", new { area = "" });

            return View();
        }

        [HttpPost]
        public ActionResult EfetuarLogin(Usuario usuarioModel)
        {
            Usuario user = UsuarioRepository.GetInstance().Get(usuarioModel.Login, usuarioModel.Senha);

            if (user != null)
            {
                // TODO: todos administradores, retirar
                user.Roles = "Administrador";
                UsuarioLogado.Usuario = user;
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else
            {
                ModelState.AddModelError("Login", Resources.Resources.Login_UsuarioOuSenhaInvalidos);
                return RedirectToAction("Index", "Login", new { area = "" });
            }
        }

        public ActionResult EsqueciMinhaSenha() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult EsqueciMinhaSenha(FormCollection frmCollection)
        {
            Usuario user = UsuarioRepository.GetInstance().Get(frmCollection["Login"]);

            if (user == null)
            {
                ViewBag.Mensagem = Resources.Resources.EsqueciMinhaSenha_UsuarioInexistente;
                return View();
            }

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587) {   EnableSsl = true };
            client.Credentials = new NetworkCredential("jkjkf2@gmail.com", "88853302xd");
            MailAddress from = new MailAddress("jkjkf2@gmail.com", "Biblioteca Plus");
            MailAddress to = new MailAddress(user.Email, user.Nome);
            MailMessage mensagem = new MailMessage(from, to);
            mensagem.Subject = "Recuperação de Senha";
            mensagem.Body = string.Format("Olá {0}.\r\nA sua senha é: {1}", user.Nome, user.Senha);

            client.Send(mensagem);

            ViewBag.Mensagem = Resources.Resources.EsqueciMinhaSenha_SenhaEnviadaComSucesso;
            
            return View();
        }

        public ActionResult Cadastrar() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Usuario usuario)
        {
            if (UsuarioRepository.GetInstance().Get(usuario.Login) != null) 
            {
                ModelState.AddModelError("Login", Resources.Resources.CRUD_Usuario_LoginJaUtilizado);
                return View(usuario);
            }
            
            UsuarioRepository.GetInstance().Add(usuario);

            return View();
        }

        public ActionResult Logoff() 
        {
            UsuarioLogado.Usuario = null;
            Session.Clear();

            return RedirectToAction("Index", "Login", new { area = "" });
        }

        public ActionResult AcessoNaoAutorizado()
        {
            return View();
        }
    }
}
