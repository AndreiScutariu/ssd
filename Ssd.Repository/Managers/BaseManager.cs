using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Ssd.Repository.Utils;

namespace Ssd.Repository.Managers
{
    public interface IBaseManager<TEnitity, in TId>
    {
        TEnitity Save(TEnitity entity);
        TEnitity Update(TEnitity entity);
        TEnitity Get(TId id);
        void Delete(TEnitity entity);
        IList<TEnitity> GetAll();
    }

    public abstract class BaseManager<TEnitity, TId> : IBaseManager<TEnitity, TId> where TEnitity : class
    {
        protected ISession Session
        {
            get { return Database.OpenSession(); }
        }

        //TODO refactor this, use ninject + singleton + nested transactions and thread safe
        public ITransactionManager TransactionManager
        {
            get { return new NHiberbateTransactionManager(); }
        }

        public TEnitity Save(TEnitity entity)
        {
            TransactionManager.RunInTransaction(() =>
            {
                entity = (TEnitity) Session.Save(entity);
            });
            return entity;
        }

        public TEnitity Update(TEnitity entity)
        {
            TransactionManager.RunInTransaction(() => Session.Update(entity));
            return entity;
        }

        public TEnitity Get(TId id)
        {
            var entity = default(TEnitity);
            TransactionManager.RunInTransaction(() =>
            {
                entity = Session.Get<TEnitity>(id);
            });
            return entity;
        }

        public void Delete(TEnitity entity)
        {
            TransactionManager.RunInTransaction(() => Session.Delete(entity));
        }

        public IList<TEnitity> GetAll()
        {
            IList<TEnitity> list = null;;
            TransactionManager.RunInTransaction(() =>
            {
                list = Session.QueryOver<TEnitity>().List();
            });
            return list == null ? new List<TEnitity>() : list.ToList();
        }
    }
}
