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
    public class MateriaisController : BaseController
    {
        private DbEntidades db = new DbEntidades();

        //
        // GET: /Materiais/

        public ActionResult Index(long? idLicao, long? idAula, byte? ordenacao)
        {
            IList<Material> materiais = new List<Material>();

            ViewBag.IdAula = idAula;
            ViewBag.IdLicao = idLicao;

            if (idAula.HasValue)
            {
                Aula aula = db.Aulas.Find(idAula.Value);
                ViewBag.Titulo = "Materiais da aula " + aula.Titulo;
                materiais = Ordenar(db.Materiais.Where(m => m.Aula.Id == idAula.Value && m.Visivel), ordenacao);
                ViewBag.RouteValues = new { idAula = idAula };
            }
            else if (idLicao.HasValue)
            {
                ViewBag.RouteValues = new { idLicao = idLicao };
                Licao licao = db.Licoes.Find(idLicao.Value);
                ViewBag.Titulo = "Materiais da lição " + licao.Titulo;
                materiais = Ordenar(db.Materiais.Where(m => m.Aula == null && m.Licao.Id == idLicao.Value && m.Visivel), ordenacao);
            }

            return View(materiais);
        }

        private IList<Material> Ordenar(IQueryable<Material> query, byte? ordenacao)
        {
            if (!ordenacao.HasValue)
                return query.ToList();

            switch ((OrdenacaoMaterial)ordenacao.Value)
            {
                case OrdenacaoMaterial.Curtidas:
                    return query.OrderByDescending(m => m.Curtidas.Count).ToList();
                case OrdenacaoMaterial.DataInclusao:
                default:
                    return query.OrderByDescending(m => m.DataInclusao).ToList();
            }
        }

        public ActionResult Visualizar(long id)
        {
            Material material = db.Materiais.Find(id);

            ViewBag.PodeCurtir = !material.Curtidas.Where(c => c.Id == UsuarioLogado.User.Id).Any();

            return View(material);
        }

        public ActionResult Curtir(long id)
        {
            Material material = db.Materiais.Find(id);
            Usuario usuario = db.Usuarios.Find(UsuarioLogado.User.Id);
            usuario.MateriaisCurtidos.Add(material);
            db.SaveChanges();

            return RedirectToAction("Visualizar", new { id = id });
        }

        public ActionResult DesfazerCurtida(long id)
        {
            Material material = db.Materiais.Find(id);
            Usuario usuario = db.Usuarios.Find(UsuarioLogado.User.Id);
            usuario.MateriaisCurtidos.Remove(material);
            db.SaveChanges();

            return RedirectToAction("Visualizar", new { id = id });
        }
    }
}
