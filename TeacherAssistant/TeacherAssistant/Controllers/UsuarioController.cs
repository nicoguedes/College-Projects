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
    public class UsuarioController : BaseController
    {
        private DbEntidades db = new DbEntidades();

        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Where(m => m.Tipo != (byte)TipoUsuario.Professor).ToList();
            usuarios.Add(db.Usuarios.Find(UsuarioLogado.User.Id));

            return View(usuarios);
        }

        //
        // GET: /Usuario/Create

        public ActionResult Create()
        {
            ViewBag.Tipo = EnumUtil.EnumTypeToSelectList(typeof(TipoUsuario));

            return View();
        }

        //
        // POST: /Usuario/Create

        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Senha = Criptografia.CriptografarMd5(usuario.Senha);
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tipo = EnumUtil.EnumTypeToSelectList(typeof(TipoUsuario));
            return View(usuario);
        }

        //
        // GET: /Usuario/Edit/5

        public ActionResult Edit(long id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //
        // POST: /Usuario/Edit/5

        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                Usuario usuarioDb = db.Usuarios.Find(usuario.Id);

                if (usuarioDb.Tipo == (byte)TipoUsuario.Professor && usuarioDb.Id != UsuarioLogado.User.Id)
                    throw new UnauthorizedAccessException("Você não possui permissão para modificar os dados de outro professor.");

                usuarioDb.Nome = usuario.Nome;
                if (!string.IsNullOrEmpty(usuario.Senha))
                    usuarioDb.Senha = Criptografia.CriptografarMd5(usuario.Senha);
                usuarioDb.Matricula = usuario.Matricula;
                usuarioDb.Email = usuario.Email;

                db.Entry(usuarioDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        //
        // GET: /Usuario/Delete/5

        public ActionResult Delete(long id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //
        // POST: /Usuario/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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