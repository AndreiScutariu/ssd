using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Ssd.Repository.Utils;

namespace Ssd.Repository.Managers
{
    public interface IBaseManager<TEnitity, TId>
    {
        TId Save(TEnitity entity);

        TEnitity Update(TEnitity entity);
        
        TEnitity Get(TId id);

        TEnitity Load(TId id);
        
        void Delete(TEnitity entity);
        
        IList<TEnitity> GetAll();
    }

    public abstract class BaseManager<TEnitity, TId> : IBaseManager<TEnitity, TId> where TEnitity : class
    {
        protected ISession Session
        {
            get { return Database.OpenSession(); }
        }

        public ITransactionManager TransactionManager
        {
            get { return new NHiberbateTransactionManager(); }
        }

        public TId Save(TEnitity entity)
        {
            var id = default(TId);
            TransactionManager.RunInTransaction(() =>
            {
                id = (TId)Session.Save(entity);
            });
            return id;
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

        public TEnitity Load(TId id)
        {
            var entity = default(TEnitity);
            TransactionManager.RunInTransaction(() =>
            {
                entity = Session.Load<TEnitity>(id);
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
