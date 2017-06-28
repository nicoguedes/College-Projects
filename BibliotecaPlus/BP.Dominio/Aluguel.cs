using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    public class Aluguel
    {
        public virtual int CodAluguel { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual DateTime DataSolicitacao { get; set; }
        public virtual decimal ValorAluguel { get; set; }
        public virtual decimal ValorMulta { get; set; }
        //TODO: Gerar Data de Entrega a partir do Prazo de Entrega retornado pelos Correios
        public virtual DateTime DataPrevistaEntrega { get; set; }
        public virtual decimal ValorFrete { get; set; }
        public virtual DateTime DataVencimentoDevolucao { get; set; }
        public virtual DateTime? DataDevolucao { get; set; }
        public virtual IList<Livro> Livros { get; set; }

        public Aluguel()
        {
            this.CalcularMulta();
        }

        public virtual void CalcularMulta()
        {
            if (DateTime.Now > this.DataVencimentoDevolucao)
            {
                var tempo = DateTime.Now.Subtract(this.DataVencimentoDevolucao);
                this.ValorMulta = Math.Round(((decimal)0.07 * this.ValorAluguel) * tempo.Days, 2);
            }
            else
                this.ValorMulta = 0;
        }
    }
}
