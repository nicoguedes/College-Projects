using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.DTO;

namespace Dominio.Factory
{
    public static class FactoryAluguel
    {
        public static AluguelDTO Criar(Carrinho carrinho, PrecoPrazoEntrega precoPrazo)
        {
            return new AluguelDTO()
            {
                Aluguel = new Aluguel()
                {
                    Livros = carrinho.Livros,
                    ValorAluguel = carrinho.Livros.Sum(m => m.ValorDia * 7),
                    Usuario = carrinho.Usuario,
                    DataSolicitacao = DateTime.Now,
                    ValorFrete = precoPrazo.Valor,
                    DataPrevistaEntrega = DateTime.Now.AddDays(precoPrazo.DiasParaEntrega),
                    DataVencimentoDevolucao = DateTime.Now.AddDays(precoPrazo.DiasParaEntrega + 7)
                },
                Carrinho = carrinho
            };

        }
    }
}
