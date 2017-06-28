using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherAssistant.Models;
using TeacherAssistant.Util;
using System.Text.RegularExpressions;
using System.IO;

namespace TeacherAssistant.Controllers
{
    public class MaterialController : BaseController
    {
        private DbEntidades db = new DbEntidades();

        //
        // GET: /Material/

        [CustomAuthorize(TipoUsuario.Professor)]
        public ActionResult Index(long? idLicao, long? idAula)
        {
            var queryMateriais = db.Materiais.Where(m => m.Professor.Id == UsuarioLogado.User.Id);

            IList<Material> materiais;

            if (idLicao.HasValue)
                materiais = queryMateriais.Where(m => m.Licao.Id == idLicao.Value).ToList();
            else if (idAula.HasValue)
                materiais = queryMateriais.Where(m => m.Aula.Id == idAula.Value).ToList();
            else
                throw new ApplicationException("Deve ser informada uma lição ou uma aula");

            return View(materiais);
        }

        //
        // GET: /Material/Create

        [CustomAuthorize]
        public ActionResult Create(long? idLicao, long? idAula)
        {
            ViewBag.Tipo = EnumUtil.EnumTypeToSelectList(typeof(TipoMaterial));

            Material material = new Material();
            material.Licao = idLicao.HasValue ? new Licao() { Id = idLicao.Value } : null;
            material.Aula = idAula.HasValue ? new Aula() { Id = idAula.Value } : null;

            return View(material);
        }

        //
        // POST: /Material/Create

        [HttpPost]
        [ValidateInput(false)]
        [CustomAuthorize]
        public ActionResult Create(Material material)
        {
            var file = Request.Files["ArquivoMaterial"];

            if (material.Tipo == (byte)TipoMaterial.Documento && string.IsNullOrEmpty(material.Descricao))
                ModelState.AddModelError("Descricao", "No tipo de Material Documento, a Descrição é obrigatória.");
            if (string.IsNullOrEmpty(material.Url))
                if (material.Tipo == (byte)TipoMaterial.Youtube || material.Tipo == (byte)TipoMaterial.Referencia)
                    ModelState.AddModelError("Url", "URL obrigatória.");

            if (ModelState.IsValid)
            {
                if (UsuarioLogado.User.Tipo == (byte)TipoUsuario.Aluno)
                {
                    material.Aluno = db.Usuarios.Find(UsuarioLogado.User.Id);
                    material.Visivel = false;
                    material.Professor = db.Usuarios.Find(UsuarioLogado.Turma.Professor.Id);
                }
                else
                    material.Professor = db.Usuarios.Find(UsuarioLogado.User.Id);

                if (material.Licao != null)
                    material.Licao = db.Licoes.Find(material.Licao.Id);
                else if (material.Aula != null)
                    material.Aula = db.Aulas.Find(material.Aula.Id);
                else
                    throw new ApplicationException("Deve ser informada uma lição ou uma aula");

                if (material.Tipo == (byte)TipoMaterial.Youtube)
                    material.Url = TratarUrlYoutube(material.Url);



                material.DataInclusao = DateTime.Now;

                db.Materiais.Add(material);
                db.SaveChanges();

                if (file != null && file.ContentLength > 0 && material.Tipo == (byte)TipoMaterial.Anexo)
                {
                    // Concatena o Id do Material ao nome do arquivo para não correr o risco de ter materiais com nomes repetidos
                    string diretorio = "~/Content/Materiais/";
                    if (!Directory.Exists(Server.MapPath(diretorio)))
                        Directory.CreateDirectory(diretorio);

                    string caminho = string.Format("{0}{1}_{2}", diretorio,  material.Id, file.FileName);
                    file.SaveAs(Server.MapPath(caminho));
                    material.Url = caminho;
                    db.Entry(material).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index", GetRouteValues(material));
            }

            ViewBag.Tipo = EnumUtil.EnumTypeToSelectList(typeof(TipoMaterial));
            return View(material);
        }

        //
        // GET: /Material/Edit/5

        [CustomAuthorize(TipoUsuario.Professor)]
        public ActionResult Edit(long id = 0)
        {
            Material material = db.Materiais.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }

            ViewBag.Tipo = EnumUtil.EnumTypeToSelectList(typeof(TipoMaterial));
            return View(material);
        }

        //
        // POST: /Material/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        [CustomAuthorize(TipoUsuario.Professor)]
        public ActionResult Edit(Material material)
        {
            if (ModelState.IsValid)
            {
                Material materialDb = db.Materiais.Find(material.Id);

                materialDb.Titulo = material.Titulo;
                materialDb.Descricao = material.Descricao;
                materialDb.Visivel = material.Visivel;

                db.Entry(materialDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", GetRouteValues(material));
            }

            ViewBag.Tipo = EnumUtil.EnumTypeToSelectList(typeof(TipoMaterial));
            return View(material);
        }

        //
        // GET: /Material/Delete/5

        [CustomAuthorize(TipoUsuario.Professor)]
        public ActionResult Delete(long id = 0)
        {
            Material material = db.Materiais.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        //
        // POST: /Material/Delete/5

        [HttpPost, ActionName("Delete")]
        [CustomAuthorize(TipoUsuario.Professor)]
        public ActionResult DeleteConfirmed(long id)
        {
            Material material = db.Materiais.Find(id);
            string url = material.Url;
            
            object routeValues = GetRouteValues(material);
            db.Materiais.Remove(material);
            db.SaveChanges();

            if (!string.IsNullOrEmpty(url) && material.Tipo == (byte)TipoMaterial.Anexo)
                System.IO.File.Delete(Server.MapPath(url));

            return RedirectToAction("Index", routeValues);
        }

        [CustomAuthorize(TipoUsuario.Professor)]
        public ActionResult VisualizarCurtidas(long id)
        {
            Material material = db.Materiais.Find(id);
            return View(material);
        }

        private object GetRouteValues(Material material)
        {
            if (material.Licao != null)
            {
                return new { idLicao = material.Licao.Id };
            }
            else if (material.Aula != null)
            {
                return new { idAula = material.Aula.Id };
            }
            else
                throw new ApplicationException("Deve ser informada uma lição ou uma aula");
        }

        private string TratarUrlYoutube(string url)
        {
            return Regex.Match(url, @"(?:youtube\.com\/(?:[^\/]+\/.+\/|(?:v|e(?:mbed)?)\/|.*[?&]v=)|youtu\.be\/)([^""&?\/ ]{11})").Groups[1].Value;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}