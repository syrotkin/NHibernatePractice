using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernatePractice1.ByCode;

namespace NHibernatePractice1.Repositories
{
    public class SessionHelper
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
                    //configuration.AddAssembly(typeof (OsyProduct).Assembly); // can add an assembly, can add a class, a configuration file. If assembly, config files have to be embeddedd resources. re
                    configuration.AddAssembly(Assembly.GetCallingAssembly());

                    //ConfigureMappings();

                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        private static void ConfigureMappings()
        {
            var osyCatMapper = new OsyCatMapper();
            osyCatMapper.MapOsyCat();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();

            // SessionFactory.GetCurrentSession();
            // ICurrentSessionContext a;
            // a.CurrentSession
        }

        public static void CloseSession()
        {
            // TODO: not sure because we don't keep a reference to ISession
            // we dispose of it every time
        }

        public static void CloseSessionFactory()
        {
            if (!SessionFactory.IsClosed)
            {
                SessionFactory.Close();
            }
        }

    }
}
