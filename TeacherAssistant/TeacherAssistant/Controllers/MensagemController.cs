using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherAssistant.Models;

namespace TeacherAssistant.Controllers
{
    public class MensagemController : BaseController
    {
        DbEntidades db = new DbEntidades();

        //
        // GET: /Mensagem/

        public ActionResult Index()
        {
            var mensagens = db.MensagensUsuario.Where(m => m.Destinatario.Id == UsuarioLogado.User.Id).OrderByDescending(m => m.DataEnvio).ToList();

            ViewBag.MensagensEnviadas = db.MensagensUsuario.Where(m => m.Remetente.Id == UsuarioLogado.User.Id).OrderByDescending(m => m.DataEnvio).ToList();

            return View(mensagens);
        }

        public ActionResult Enviar(long id, string assunto)
        {
            MensagemUsuario mensagem = new MensagemUsuario()
            {
                Destinatario = db.Usuarios.Find(id),
                Assunto = assunto
            };

            return View(mensagem);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Enviar(MensagemUsuario mensagem)
        {
            mensagem.Destinatario = db.Usuarios.Find(mensagem.Destinatario.Id);
            mensagem.Remetente = db.Usuarios.Find(UsuarioLogado.User.Id);
            mensagem.DataEnvio = DateTime.Now;

            db.MensagensUsuario.Add(mensagem);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Visualizar(long id)
        {
            var mensagem = db.MensagensUsuario.Find(id);
            if (mensagem.Destinatario.Id != UsuarioLogado.User.Id)
                throw new UnauthorizedAccessException("Você não possui permissão para acessar esta mensagem.");

            if (!mensagem.Lida)
            {
                mensagem.Lida = true;
                db.Entry(mensagem);
                db.SaveChanges();
            }

            return View(mensagem);
        }
    }
}
