using System;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

using Environment = NHibernate.Cfg.Environment;

namespace Tests.Unit {

    public class InMemoryDatabaseForXmlMappings : IDisposable {

        protected Configuration m_config;
        protected ISessionFactory m_sessionFactory;


        public InMemoryDatabaseForXmlMappings() {
            m_config = new Configuration()
                    .SetProperty(Environment.ReleaseConnections, "on_close")
                    .SetProperty(Environment.Dialect, typeof(SQLiteDialect).AssemblyQualifiedName)
                    .SetProperty(Environment.ConnectionDriver, typeof(SQLite20Driver).AssemblyQualifiedName)
                    .SetProperty(Environment.ConnectionString, "data source=:memory:")
                    .AddFile("../../../Persistence/Mappings/Xml/Employee.hbm.xml")
                    .AddFile("../../../Persistence/Mappings/Xml/Address.hbm.xml")
                    .AddFile("../../../Persistence/Mappings/Xml/Community.hbm.xml")
                    .AddFile("../../../Persistence/Mappings/Xml/Benefit.hbm.xml");
            m_sessionFactory = m_config.BuildSessionFactory();
            Session = m_sessionFactory.OpenSession();

            new SchemaExport(m_config).Execute(true, true, false, Session.Connection, Console.Out);
        }

        public ISession Session {
            get;
            set;
        }

        public void Dispose() {
             Session.Dispose();
             m_sessionFactory.Dispose();
        }

    }

}