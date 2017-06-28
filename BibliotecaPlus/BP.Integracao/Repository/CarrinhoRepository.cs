using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio;
using Integracao.Utils;
using NHibernate;
using BP.Resources;

namespace Integracao.Repository
{
	public class CarrinhoRepository : RepositoryBase<Carrinho>
	{
		private static CarrinhoRepository instancia;

		public static CarrinhoRepository GetInstance()
		{
			if (instancia == null)
				instancia = new CarrinhoRepository();

			return instancia;
		}

        public void RemoverLivroDoCarrinho(int codigoLivro, Usuario user) 
        {
            Carrinho carrinho = Get(user);

            if (carrinho == null)
                throw new ApplicationException(Resources.Carrinho_Inexistente);

            if (carrinho.Livros == null || carrinho.Livros.Count <= 0)
                return;

            carrinho.Livros = carrinho.Livros.Where(m => m.CodLivro != codigoLivro).ToList();

            this.Update(carrinho);
        }

		public void AdicionarLivroAoCarrinho(int codigoLivro, Usuario user)
		{
			Carrinho carrinho = Get(user);

			bool add = false;

			if (carrinho == null)
			{
				add = true;
				carrinho = new Carrinho() { Usuario = user };
			}

			Livro livro = LivroRepository.GetInstance().GetLivro(codigoLivro);
			if (livro == null)
                throw new ApplicationException(BP.Resources.Resources.Carrinho_CodigoLivroInvalido);

            if (livro.QuantidadeDisponivel <= 0)
                throw new ApplicationException(BP.Resources.Resources.Carrinho_LivroNaoDisponivel);

			if (carrinho.Livros == null)
				carrinho.Livros = new List<Livro>();

            foreach (Livro lv in carrinho.Livros)
                if (lv.CodLivro == codigoLivro)
                    throw new ApplicationException(BP.Resources.Resources.Carrinho_LivroJaAdicionado);

			carrinho.Livros.Add(livro);

			if (add)
				this.Add(carrinho);
			else
				this.Update(carrinho);
		}

        public void LimparCarrinho(Usuario user) 
        {
            Carrinho carrinho = Get(user);

            if (carrinho == null || carrinho.Livros == null || carrinho.Livros.Count <= 0)
                return;

            carrinho.Livros.Clear();

            this.Update(carrinho);
        }

		public Carrinho Get(Usuario user)
		{
			using (ISession session = NHibernateUtils.OpenSession())
			{
				return session.QueryOver<Carrinho>().Where(x => x.Usuario == user).List().FirstOrDefault();
			}
		}
	}
}
