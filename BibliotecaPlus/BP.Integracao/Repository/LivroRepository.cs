using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Integracao.Utils;
using Dominio;
using NHibernate;

namespace Integracao.Repository
{
	public class LivroRepository : RepositoryBase<Livro>
	{
		private static LivroRepository instancia;

		public static LivroRepository GetInstance() 
		{
			if (instancia == null)
				instancia = new LivroRepository();

			return instancia;
		}

		public Livro GetLivro(int id) 
		{
			using (ISession session = NHibernateUtils.OpenSession())
			{
				return session.QueryOver<Livro>().Where(x => x.CodLivro == id).List().FirstOrDefault();
			}
		}

		public IList<Livro> GetLivrosEmDestaque(int quantidade) 
		{
			using (ISession session = NHibernateUtils.OpenSession())
			{
				return session.QueryOver<Livro>().Where(x => x.QuantidadeDisponivel > 0).Take(quantidade).List();
			}
		}

		public IList<Livro> GetLivrosPorNome(string titulo)
		{
			using (ISession session = NHibernateUtils.OpenSession())
			{
				return session.QueryOver<Livro>().Where(x => x.Titulo == titulo).List();
			}
		}

		public IList<Livro> GetLivrosPorGenero(Generos genero) 
		{
			using (ISession session = NHibernateUtils.OpenSession())
			{
				return session.QueryOver<Livro>().Where(x => x.Genero  == genero).List();
			}
		}
	}
}
