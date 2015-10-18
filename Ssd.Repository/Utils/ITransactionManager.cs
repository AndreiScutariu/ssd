using System;

namespace Ssd.Repository.Utils
{
    public interface ITransactionManager
    {
        void RunInTransaction(Action action);
    }
}