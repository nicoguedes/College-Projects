using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherAssistant.Models;
using TeacherAssistant.ViewModels;
using TeacherAssistant.Util;
using System.IO;

namespace TeacherAssistant.Controllers
{
    [CustomAuthorize]
    public class PerfilController : BaseController
    {
        DbEntidades db = new DbEntidades();

        //
        // GET: /Perfil/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TrocarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TrocarSenha(TrocaSenha trocaSenha)
        {
            if (ModelState.IsValid)
            {
                if (trocaSenha.ConfirmacaoNovaSenha != trocaSenha.NovaSenha)
                {
                    ModelState.AddModelError("ConfirmacaoNovaSenha", "As senhas não batem.");
                    return View();
                }


                trocaSenha.SenhaAtual = Criptografia.CriptografarMd5(trocaSenha.SenhaAtual);
                Usuario usuario = db.Usuarios.Where(m => m.Id == UsuarioLogado.User.Id && m.Senha == trocaSenha.SenhaAtual).FirstOrDefault();
                if (usuario == null)
                {
                    ModelState.AddModelError("SenhaAtual", "Senha inválida.");
                    return View();
                }

                usuario.Senha = Criptografia.CriptografarMd5(trocaSenha.NovaSenha);
                db.Entry(usuario);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(); ;
        }

        public ActionResult Visualizar(long id)
        {
            Usuario usuario = db.Usuarios.Find(id);

            return View(usuario);
        }

        public ActionResult MinhaTurma()
        {
            Turma turma = db.Turmas.Find(UsuarioLogado.Turma.Id);
            return View(turma);
        }

        [HttpPost]
        public ActionResult TrocarFoto(AlteracaoFoto model)
        {
            var foto = Request.Files["Foto"];

            if (foto != null && foto.ContentLength > 0)
            {
                Usuario usuario = db.Usuarios.Find(UsuarioLogado.User.Id);
                string diretorio = "~/Content/Fotos/";
                if (!Directory.Exists(Server.MapPath(diretorio)))
                    Directory.CreateDirectory(diretorio);

                string caminho = string.Format("{0}{1}_{2}", diretorio, usuario.Id, foto.FileName);

                if (System.IO.File.Exists(caminho))
                    System.IO.File.Delete(caminho);

                foto.SaveAs(Server.MapPath(caminho));

                usuario.CaminhoFoto = caminho;
                db.Entry(usuario);
                db.SaveChanges();
            }

            return RedirectToAction("Visualizar", new { id = UsuarioLogado.User.Id });
        }

    }
}
