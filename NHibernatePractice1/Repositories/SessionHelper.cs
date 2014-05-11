﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernatePractice1.Domain;
using NHibernate.Context;

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
                    configuration.AddAssembly(typeof (OsyProduct).Assembly); // TODO: This seems important
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
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