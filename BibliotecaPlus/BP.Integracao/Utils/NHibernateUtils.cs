using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using Dominio;
using NHibernate.Tool.hbm2ddl;
using System.IO;
using System.Reflection;

namespace Integracao.Utils
{
	public class NHibernateUtils
	{
		private static ISessionFactory _sessionFactory;

		private static ISessionFactory SessionFactory
		{
			get
			{
				if (_sessionFactory == null)
				{
					var configuration = new Configuration();
					configuration.Configure();
					configuration.AddAssembly(typeof(Livro).Assembly);
					_sessionFactory = configuration.BuildSessionFactory();
				}
				return _sessionFactory;
			}
		}

		public static ISession OpenSession()
		{
			return SessionFactory.OpenSession();
		}

		public static void AbrirConexao(bool criarDatabase)
		{
			var cfg = new Configuration();
			cfg.Configure();
			cfg.AddAssembly(typeof(Livro).Assembly);

			if (criarDatabase)
			{
				SchemaExport sch = new SchemaExport(cfg);

				sch.Execute(true, true, false);
			}
		}
	}
}
