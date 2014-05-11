using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernatePractice1.Repositories;
using NHibernatePractice1.Domain;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace NHibernatePracticeTest
{
    public class OsyCatTester
    {
        public void Run()
        {
           ExportSchema();
           InsertCat();
          // InsertOsyProduct();
        }

        private void InsertOsyProduct()
        {
            var osyProduct = new OsyProduct {Name = "Coke", Category = "Drinks"};
            Insert(osyProduct);
            Console.WriteLine("done inserting product");
        }


        private void InsertCat()
        {
            var cat = new OsyCat { Name = "Tom", Sex = 'M', Weight = 3.5f };
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
