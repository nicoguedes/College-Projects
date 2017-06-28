using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.DTO;
using Integracao.Correios.CalcPrecoPrazoWS;

namespace Integracao.Repository
{
    public class CalculoPrecoPrazoRepository
    {
        private static Correios.CalcPrecoPrazoWS.CalcPrecoPrazoWS _soapClient;
        private static CalculoPrecoPrazoRepository _instancia;

        private const string PesoDefaultLivro = "5";
        private const int CodFormatoDefault = 1;
        private const decimal ComprimentoDefault = 30;
        private const decimal AlturaDefault = 10;
        private const decimal DiametroDefault = 30;
        private const decimal LarguraDefault = 30;


        public static CalculoPrecoPrazoRepository GetInstance()
        {
            if (_instancia == null)
                _instancia = new CalculoPrecoPrazoRepository();

            return _instancia;
        }

        public PrecoPrazoEntrega CalcularPrecoPrazo(string cepDestino)
        {
            return CalcularPrecoPrazo("41106", "32110050", cepDestino);
        }

        public PrecoPrazoEntrega CalcularPrecoPrazo(string codigoServico, string cepOrigem, string cepDestino)
        {
            try
            {
                if (_soapClient == null)
                    _soapClient = new CalcPrecoPrazoWS();

                System.Net.ServicePointManager.Expect100Continue = false;

                cResultado result = _soapClient.CalcPrecoPrazo("", "", codigoServico, cepOrigem, cepDestino, PesoDefaultLivro, CodFormatoDefault, ComprimentoDefault, AlturaDefault, LarguraDefault, DiametroDefault, "N", 0, "N");

                // TODO: incluir mensagem ao resources
                if (result == null || result.Servicos == null || result.Servicos.Length <= 0)
                    throw new ArgumentNullException("Não chegou nenhum retorno do Webservice dos Correios.");

                if (!string.IsNullOrEmpty(result.Servicos.First().Erro) && !string.IsNullOrEmpty(result.Servicos.First().MsgErro))
                    throw new ApplicationException(result.Servicos.First().MsgErro);

                return new PrecoPrazoEntrega()
                {
                    DiasParaEntrega = int.Parse(result.Servicos[0].PrazoEntrega),
                    Valor = decimal.Parse(result.Servicos[0].Valor)
                };
            }
            catch
            {
                // Qualquer exceção que ocorrer, retorna um dado default, para o sistema não parar em caso do Correios estar offline, hehe
                return new PrecoPrazoEntrega() { DiasParaEntrega = new Random().Next(1, 5), Valor = (decimal)(new Random().NextDouble() * 20) };
            }
        }
    }
}
