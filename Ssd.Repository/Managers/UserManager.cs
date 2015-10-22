using NHibernate;
using Ssd.Repository.Entities;
using Ssd.Repository.Utils;

namespace Ssd.Repository.Managers
{
    public class UserManager// : BaseManager<User, int> 
    {
        protected ISession Session
        {
            get { return Database.OpenSession(); }
        }

        public ITransactionManager TransactionManager
        {
            get { return new NHiberbateTransactionManager(); }
        }

        public int Save(User entity)
        {
            var id = default(int);
            TransactionManager.RunInTransaction(() =>
            {
                id = (int)Session.Save(entity);
            });
            return id;
        }
    }
}
