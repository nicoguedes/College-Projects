using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherAssistant.Models;
using TeacherAssistant.Util;

namespace TeacherAssistant.Controllers
{
    [CustomAuthorize]
    public class ForumController : BaseController
    {
        DbEntidades db = new DbEntidades();
        //
        // GET: /Forum/

        public ActionResult Index(long id)
        {
            string titulo;

            var aula = db.Aulas.Where(a => a.Forum.Id == id).FirstOrDefault();
            if (aula == null)
                titulo = db.Licoes.Where(l => l.Forum.Id == id).First().Titulo;
            else
                titulo = aula.Titulo;

            ViewBag.ForumId = id;
            ViewBag.Titulo = titulo;

            return View(db.Mensagens.Where(m => m.Forum.Id == id && m.Pai == null).OrderByDescending(m => m.DataEnvio).ToList());
        }

        public ActionResult VisualizarMensagem(long id)
        {
            Mensagem mensagem = db.Mensagens.Find(id);
            return View(mensagem);
        }

        public ActionResult EnviarMensagem(long? idPai, long idForum)
        {
            Mensagem mensagem = new Mensagem();

            if (idPai.HasValue)
            {
                mensagem.Pai = db.Mensagens.Find(idPai.Value);
                ViewBag.Title = "Responder: " + mensagem.Pai.Assunto;
            }

            if (string.IsNullOrEmpty(ViewBag.Title))
                ViewBag.Title = "Nova mensagem";

            mensagem.Forum = db.Foruns.Find(idForum);

            return View(mensagem);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EnviarMensagem(Mensagem mensagem)
        {
            mensagem.DataEnvio = DateTime.Now;
            if (mensagem.Pai != null)
                mensagem.Pai = db.Mensagens.Find(mensagem.Pai.Id);
            mensagem.Usuario = db.Usuarios.Find(UsuarioLogado.User.Id);
            mensagem.Forum = db.Foruns.Find(mensagem.Forum.Id);

            db.Mensagens.Add(mensagem);
            db.SaveChanges();

            if (mensagem.Pai != null)
                return RedirectToAction("VisualizarMensagem", new { id = mensagem.Pai.Id });
            else
                return RedirectToAction("Index", new { id = mensagem.Forum.Id });
        }
    }
}
