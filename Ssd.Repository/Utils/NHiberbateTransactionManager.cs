using System;
using NHibernate;

namespace Ssd.Repository.Utils
{
    public class NHiberbateTransactionManager : ITransactionManager
    {
        private bool _isOpenedTransaction; 

        private ISession Session
        {
            get { return Database.OpenSession(); }
        }

        public void RunInTransaction(Action actionToExecute)
        {
            if (_isOpenedTransaction)
            {
                actionToExecute();
            }

            using (var tx = Session.BeginTransaction())
            {
                try
                {
                    _isOpenedTransaction = true;
                    actionToExecute();
                    tx.Commit();
                }
                catch (Exception)
                {
                    tx.Rollback();
                    throw;
                }
                finally
                {
                    _isOpenedTransaction = false;
                }
            }
        }
    }
}