﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Ssd.Repository.Entities;
using Ssd.Repository.Security.Interceptors;

namespace Ssd.Repository
{
    public class Database
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();
                return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(@"Data Source=ANDREISCT\SQLEXPRESS;Initial Catalog=SsdDb;Integrated Security=True"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Base<int>>())
                .ExposeConfiguration(config => config.SetInterceptor(new NHibernateIterceptor()))
                //.ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, true, false))
                .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}