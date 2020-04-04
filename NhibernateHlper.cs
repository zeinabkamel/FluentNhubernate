using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class NhibernateHlper
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory sessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    IntielizeSeessionFactroy();
                }
                return _sessionFactory;
            }
        }

        private static void IntielizeSeessionFactroy()
        {
            _sessionFactory = Fluently.Configure()
             .Database(MsSqlConfiguration.MsSql2012.ConnectionString(@"Server=.;DataBase=carDB;Integrated Security=true;")
                           .ShowSql()
             )
             .Mappings(m =>
                       m.FluentMappings
                           .AddFromAssemblyOf<Car>())
             .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
             .BuildSessionFactory();


        }
        public static ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }
    }
}
