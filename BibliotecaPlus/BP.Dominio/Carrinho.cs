using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
	public class Carrinho
	{
		public virtual int CodCarrinho { get; set; }
		public virtual Usuario Usuario { get; set; }
		public virtual IList<Livro> Livros { get; set; }
	}
}