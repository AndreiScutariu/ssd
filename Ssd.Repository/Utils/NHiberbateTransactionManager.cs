using System;
using NHibernate;

namespace Ssd.Repository.Utils
{
    public class NHiberbateTransactionManager : ITransactionManager
    {
        private ISession Session
        {
            get { return Database.OpenSession(); }
        }

        public void RunInTransaction(Action action)
        {
            using (var tx = Session.BeginTransaction())
            {
                try
                {
                    action();
                    tx.Commit();
                }
                catch (Exception)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}