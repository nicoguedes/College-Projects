using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BP.Web.Models;

namespace BP.Web.Controllers
{
    public class HomeController : Controller
    {
        [BPAuthorize]
        public ActionResult Index()
        {
            //Integracao.Repository.AutorRepository repository = new Integracao.Repository.AutorRepository();
            //repository.Add(new Dominio.Autor() { Nome = "Paulo Coelho" });
            //IList<Dominio.Autor> autores = repository.GetAll();

            //Integracao.Repository.LivroRepository.GetInstance().Add(new Dominio.Livro() { Autor = autores.FirstOrDefault(), Titulo = "O Alquimista" });

            //Integracao.Repository.AluguelRepository aluguelRepo = new Integracao.Repository.AluguelRepository();
            //aluguelRepo.Add(new Dominio.Aluguel() { DataSolicitacao = DateTime.Now, DataPrevistaEntrega = DateTime.Now.AddDays(3), ValorAluguel = 7, ValorFrete = 12, DataVencimentoDevolucao = DateTime.Now.AddDays(7) });

            //var retPrazo = Integracao.Repository.CalculoPrecoPrazoRepository.GetInstance().CalcularPrecoPrazo("32210050");

            return View();
        }

        public ActionResult ModificarIdioma(string idioma)
        {
            HttpCookie cookie = new HttpCookie("CurrentCulture");
            cookie.Value = idioma;
            cookie.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
