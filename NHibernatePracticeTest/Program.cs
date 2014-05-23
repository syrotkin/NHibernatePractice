using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernatePractice1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernatePractice1.Repositories;

namespace NHibernatePracticeTest
{
    // Write a comment here from SVN.
    class Program
    {
        static void Main(string[] args)
        {
            //log4net.Config.XmlConfigurator.Configure();
            
            var catTester = new OsyCatTester();
            catTester.Run();
            Console.WriteLine("Done...");
            Console.ReadLine();
        }

        private static void ConfigureSession()
        {
            var configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(OsyProduct).Assembly);
            ISessionFactory sessionFactory
                = configuration.BuildSessionFactory();
        }

        // Drops and recreates tables for types defined in mapping.
        private static void TestExportSchema()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(OsyProduct).Assembly);
         
            new SchemaExport(cfg).Execute(true, true, false);
            
            Console.WriteLine("Done");
        }
    }
}
