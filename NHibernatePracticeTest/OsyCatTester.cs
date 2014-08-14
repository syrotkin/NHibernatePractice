using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernatePractice1.Domain;
using NHibernatePractice1.Repositories;

namespace NHibernatePracticeTest
{
    public class OsyCatTester
    {
        public void Run()
        {
            LoadCatsWithQueryOver();

            //ExportSchema(); // don't have to do it every time. Creating tables once should be enough.
            //InsertCat();
            // InsertOsyProduct();
        }

        private void LoadCatsWithQueryOver()
        {
            using (ISession session = SessionHelper.OpenSession())
            {
                // TODO: no transaction here?
                IList<OsyCat> cats = session.QueryOver<OsyCat>()
                    .Where(cat => cat.Name == "Jill")
                    .And(cat => cat.Weight < 5)
                    .OrderBy(cat => cat.Name).Asc
                    .List();

                foreach (OsyCat cat in cats)
                {
                    Console.WriteLine(cat);
                }
            }
        }

        private void InsertOsyProduct()
        {
            var osyProduct = new OsyProduct { Name = "Coke", Category = "Drinks" };
            Insert(osyProduct);
            Console.WriteLine("done inserting product");
        }


        private void InsertCat()
        {
            var cat = new OsyCat { Name = "Bob", Sex = 'M', Weight = 3.4f };
            Insert(cat);
            Console.WriteLine("done inserting cat");
        }

        private void Insert(object item)
        {
            using (ISession session = SessionHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(item);
                    transaction.Commit();
                }
            }
        }

        private void ExportSchema()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(OsyCat).Assembly);

            new SchemaExport(cfg).Execute(true, true, false);

            Console.WriteLine("Schema exported.");
        }



        private void GetCat()
        {
            using (ISession session = SessionHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    // HQL
                    IQuery query = session.CreateQuery("select cat from OsyCat as cat where cat.Name = :name");
                    query.SetString("name", "Tom");
                    foreach (OsyCat osyCat in query.Enumerable())
                    {
                        Console.WriteLine("Cat: " + osyCat.Name);
                    }
                    transaction.Commit();
                }
            }
        }
    }
}
