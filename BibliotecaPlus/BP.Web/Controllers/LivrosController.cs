using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Integracao.Repository;
using Dominio;
using BP.Web.Models;

namespace BP.Web.Controllers
{
	public class LivrosController : Controller
	{
		//
		// GET: /Livros/

		[BPAuthorize]
		public ActionResult Index(int? genero)
		{
			IList<Livro> livros = null;

			if (!genero.HasValue)
				livros = LivroRepository.GetInstance().GetLivrosEmDestaque(10);
			else
			{
				if (!Enum.IsDefined(typeof(Generos), genero))
					throw new ApplicationException("Gênero selecionado inválido.");

				livros = LivroRepository.GetInstance().GetLivrosPorGenero((Generos)genero);
			}

			return View(livros);
		}
	}
}
