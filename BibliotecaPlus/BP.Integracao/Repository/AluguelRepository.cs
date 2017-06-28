using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio;
using Integracao.Utils;
using NHibernate;

namespace Integracao.Repository
{
    public class AluguelRepository : RepositoryBase<Aluguel>
    {
        private static AluguelRepository _instancia;

        public static AluguelRepository Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new AluguelRepository();
                return _instancia;
            }
        }

        public Aluguel Get(int codigo)
        {
            using (ISession session = NHibernateUtils.OpenSession())
            {
                return session.QueryOver<Aluguel>().Where(x => x.CodAluguel == codigo).List().FirstOrDefault();
            }
        }

        public void ConfirmarAluguel(Aluguel aluguel)
        {
            // TODO: incluir mensagem ao resources
            if (aluguel.Livros == null || aluguel.Livros.Count <= 0)
                throw new ApplicationException("Nenhum livro selecionado para o aluguel.");

            using (ISession session = NHibernateUtils.OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {
                    session.Save(aluguel);

                    foreach (Livro livro in aluguel.Livros)
                    {
                        livro.QuantidadeDisponivel--;
                        LivroRepository.GetInstance().Update(livro);
                    }

                    CarrinhoRepository.GetInstance().LimparCarrinho(aluguel.Usuario);

                    tran.Commit();
                }
            }
        }

        public void RegistrarDevolucao(Aluguel aluguel)
        {
            aluguel.DataDevolucao = DateTime.Now;

            using (ISession session = NHibernateUtils.OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {
                    session.Update(aluguel);

                    foreach (Livro livro in aluguel.Livros)
                    {
                        livro.QuantidadeDisponivel++;
                        LivroRepository.GetInstance().Update(livro);
                    }

                    tran.Commit();
                }
            }
        }
    }
}
