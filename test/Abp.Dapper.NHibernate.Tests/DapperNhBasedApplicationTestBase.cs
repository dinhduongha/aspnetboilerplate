using System;
using System.Data.Common;
using System.Data.SQLite;

using Abp.TestBase;

using Castle.MicroKernel.Registration;

using NHibernate;

namespace Abp.Dapper.NHibernate.Tests
{
    public class DapperNhBasedApplicationTestBase : AbpIntegratedTestBase<AbpDapperNhBasedTestModule>
    {
        private SQLiteConnection _connection;

        protected DapperNhBasedApplicationTestBase()
        {
            AbpSession.UserId = new Guid("0171ac9e-a5ec-0851-09c7-7a53338a7a00");
            AbpSession.TenantId = 1;
        }

        protected override void PreInitialize()
        {
            _connection = new SQLiteConnection("data source=:memory:");
            _connection.Open();

            LocalIocManager.IocContainer.Register(
                Component.For<DbConnection>().Instance(_connection).LifestyleSingleton()
            );
        }

        public void UsingSession(Action<ISession> action)
        {
            using (ISession session = LocalIocManager.Resolve<ISessionFactory>().WithOptions().Connection(_connection).OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    action(session);
                    session.Flush();
                    transaction.Commit();
                }
            }
        }

        public T UsingSession<T>(Func<ISession, T> func)
        {
            T result;

            using (ISession session = LocalIocManager.Resolve<ISessionFactory>().WithOptions().Connection(_connection).OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    result = func(session);
                    session.Flush();
                    transaction.Commit();
                }
            }

            return result;
        }

        public override void Dispose()
        {
            _connection.Dispose();
            base.Dispose();
        }
    }
}
