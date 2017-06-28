using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BP.Web.Models;
using Integracao.Repository;
using Dominio;
using Dominio.DTO;
using Dominio.Factory;

namespace BP.Web.Controllers
{
    public class CarrinhoController : Controller
    {
        [BPAuthorize]
        public ActionResult Index() 
        {
            Carrinho carrinho = CarrinhoRepository.GetInstance().Get(UsuarioLogado.Usuario);

            PrecoPrazoEntrega precoPrazo = CalculoPrecoPrazoRepository.GetInstance().CalcularPrecoPrazo(UsuarioLogado.Usuario.Cep.ToString("00000000"));
            AluguelDTO aluguel = FactoryAluguel.Criar(carrinho, precoPrazo);

            Session["Aluguel"] = aluguel.Aluguel;

            return View(aluguel);
        }

        [BPAuthorize]
        [HttpPost]
        public JsonResult AdicionarAoCarrinho(int codigoLivro)
        {
            try
            {
                CarrinhoRepository.GetInstance().AdicionarLivroAoCarrinho(codigoLivro, UsuarioLogado.Usuario);

                return Json(new { Mensagem = BP.Resources.Resources.Carrinho_LivroAdicionadoComSucesso });
            }
            catch (Exception ex)
            {
                return Json(new { Mensagem = ex.Message });
            }
        }

        [BPAuthorize]
        [HttpPost]
        public JsonResult RemoverDoCarrinho(int codigoLivro) 
        {
            try
            {
                CarrinhoRepository.GetInstance().RemoverLivroDoCarrinho(codigoLivro, UsuarioLogado.Usuario);

                return Json(new { Mensagem = BP.Resources.Resources.Carrinho_LivroRemovidoComSucesso });
            }
            catch (Exception ex)
            {
                return Json(new { Mensagem = ex.Message });
            }
        }

        [BPAuthorize]
        [HttpPost]
        public ActionResult EfetuarAluguel() 
        {
            try
            {
                Aluguel aluguel = null;

                if (Session["Aluguel"] != null)
                    aluguel = (Aluguel)Session["Aluguel"];
                else {
                    Carrinho carrinho = CarrinhoRepository.GetInstance().Get(UsuarioLogado.Usuario);

                    PrecoPrazoEntrega precoPrazo = CalculoPrecoPrazoRepository.GetInstance().CalcularPrecoPrazo(UsuarioLogado.Usuario.Cep.ToString("00000000"));
                    AluguelDTO aluguelDTO = FactoryAluguel.Criar(carrinho, precoPrazo);

                    aluguel = aluguelDTO.Aluguel;
                }

                AluguelRepository.Instancia.ConfirmarAluguel(aluguel);

                return View(aluguel);
            }
            catch (Exception ex)
            {
                return Json(new { Mensagem = ex.Message });
            }
        }
    }
}
