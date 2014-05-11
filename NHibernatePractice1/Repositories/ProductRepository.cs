using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernatePractice1.Domain;

namespace NHibernatePractice1.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private void RunTransaction(Action<ISession, OsyProduct> command, OsyProduct product)
        {
            using (ISession session = SessionHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    command(session, product);
                    transaction.Commit(); // NOTE: also could call session.Transation.Commit();
                }
            }
        }

        public void Add(OsyProduct product)
        {
            RunTransaction(AddHelper, product);
        }

        private void AddHelper(ISession session, OsyProduct product)
        {
            session.Save(product);
        }

        public void Update(OsyProduct product)
        {
            RunTransaction(UpdateHelper, product);
        }

        private void UpdateHelper(ISession session, OsyProduct product)
        {
            session.Update(product);
        }

        public void Remove(OsyProduct product)
        {
            RunTransaction(RemoveHelper, product);
        }

        private void RemoveHelper(ISession session, OsyProduct product)
        {
            session.Delete(product);
        }

        public OsyProduct GetById(Guid productId)
        {
            OsyProduct result = null;
            using (ISession session = SessionHelper.OpenSession()) // TODO: no transaction here ?
            {
                result = session.Get<OsyProduct>(productId);
            }
            return result;
        }

        public OsyProduct GetByName(string name)
        {
            OsyProduct result = null;
            using (ISession session = SessionHelper.OpenSession()) // TODO: No transaction
            {
                result = session.Query<OsyProduct>().FirstOrDefault(p => string.Equals(p.Name, name));
            }
            return result;
        }

        // HCQ
        public OsyProduct GetByNameCriteria(string name)
        {
            OsyProduct result = null;
            using (ISession session = SessionHelper.OpenSession())
            {
                result = session.CreateCriteria(typeof (OsyProduct))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<OsyProduct>();
            }
            return result;
        }


        public ICollection<OsyProduct> GetByCategory(string category)
        {
            ICollection<OsyProduct> result = null;
            using (ISession session = SessionHelper.OpenSession())  // TODO: No transaction
            {
                result = session.Query<OsyProduct>().Where(p => string.Equals(p.Category, category)).ToList();
            }
            return result;
        }

        public ICollection<OsyProduct> GetByCategoryCriteria(string category)
        {
            ICollection<OsyProduct> result = null;
            using (ISession session = SessionHelper.OpenSession())
            {
                result = session.CreateCriteria<OsyProduct>().Add(Restrictions.Eq("Category", category)).List<OsyProduct>();
            }
            return result;
        }
    }
}
