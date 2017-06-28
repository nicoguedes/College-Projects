using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
	public class Livro
	{
		public virtual int CodLivro { get; set; }
		public virtual string Titulo { get; set; }
		public virtual string Sinopse { get; set; }
		public virtual Generos Genero { get; set; }
		public virtual int QuantidadeDisponivel { get; set; }
		public virtual string Autor { get; set; }
		public virtual decimal ValorDia { get; set; }
        public virtual IList<Aluguel> Alugueis { get; set; }
	}
}