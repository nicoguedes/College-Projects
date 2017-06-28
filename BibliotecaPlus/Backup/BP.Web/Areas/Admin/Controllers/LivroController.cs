using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Integracao.Repository;
using Dominio;
using BP.Web.Models;

namespace BP.Web.Areas.Admin.Controllers
{
    public class LivroController : Controller
    {
        //
        // GET: /Livro/

        [BPAuthorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View(LivroRepository.GetInstance().GetAll());
        }

        //
        // GET: /Livro/Edit/5

        [BPAuthorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
                return View(LivroRepository.GetInstance().GetLivro(id.Value));
            else
                return View();
        }

        //
        // POST: /Livro/Edit/5

        [BPAuthorize(Roles = "Administrador")]
        [HttpPost]
        public ActionResult Edit(Livro livro)
        {
            try
            {
                if (livro.CodLivro > 0)
                    LivroRepository.GetInstance().Update(livro);
                else
                    LivroRepository.GetInstance().Add(livro);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Livro/Delete/5

        [BPAuthorize(Roles = "Administrador")]
        public ActionResult Delete(int id)
        {
            return View(LivroRepository.GetInstance().GetLivro(id));
        }

        //
        // POST: /Livro/Delete/5

        [BPAuthorize(Roles = "Administrador")]
        [HttpPost]
        public ActionResult Delete(int id, Livro livro)
        {
            try
            {
                LivroRepository.GetInstance().Delete(livro);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
