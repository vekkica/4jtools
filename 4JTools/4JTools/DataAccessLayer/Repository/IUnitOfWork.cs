using System.Transactions;

namespace _4JTools.DataAccessLayer.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges(bool acceptAllChangesOnSuccess = true);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void DoInTransaction(Action<IUnitOfWork> action, IsolationLevel level = IsolationLevel.ReadCommitted);

        void DoInTransactionScope(Action<IUnitOfWork> action, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}
