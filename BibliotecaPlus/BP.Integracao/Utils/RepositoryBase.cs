using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Collections;


namespace Integracao.Utils
{
	public abstract class RepositoryBase<T> where T : class
	{
		public virtual void Add(T item)
		{
			using (ISession session = NHibernateUtils.OpenSession())
			{
				using (ITransaction tran = session.BeginTransaction())
				{
					session.Save(item);
					tran.Commit();
				}
			}
		}

		public virtual void Update(T item)
		{
			using (ISession session = NHibernateUtils.OpenSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					session.Update(item);
					transaction.Commit();
				}
			}
		}

		public virtual void Delete(T item)
		{
			using (ISession session = NHibernateUtils.OpenSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					session.Delete(item);
					transaction.Commit();
				}
			}
		}

		public virtual IList<T> GetAll() 
		{
			using (ISession session = NHibernateUtils.OpenSession())
			{
					return session.QueryOver<T>().List();
			}
		}
	}
}
