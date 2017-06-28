using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio;
using Integracao.Utils;
using NHibernate;

namespace Integracao.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>
    {
        private static UsuarioRepository instancia;

        public static UsuarioRepository GetInstance()
        {
            if (instancia == null)
                instancia = new UsuarioRepository();

            return instancia;
        }

        public Usuario Get(string userName, string password)
        {
            using (ISession session = NHibernateUtils.OpenSession())
            {
                Usuario user = session.QueryOver<Usuario>().Where(x => x.Login == userName && x.Senha == password).List().FirstOrDefault();
                if (user != null)
                    user.Senha = null;

                return user;
            }
        }

        public Usuario Get(string userName)
        {
            using (ISession session = NHibernateUtils.OpenSession())
            {
                return session.QueryOver<Usuario>().Where(x => x.Login == userName).List().FirstOrDefault();
            }
        }
    }
}
