namespace _4JTools.DataAccessLayer.Repository
{
    public interface IRepository<TEntity>
    {
        TEntity Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void AddRange(params TEntity[] entities);

        TEntity Attach(TEntity entity);

        void AttachRange(IEnumerable<TEntity> entities);

        void AttachRange(params TEntity[] entities);

        TEntity Find(params object[] keyValues);

        ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);

        ValueTask<TEntity> FindAsync(params object[] keyValues);

        IQueryable<TEntity> Get(bool tracking = false);
        IQueryable<TEntity> GetHistory();

        IQueryable<TEntity> GetHistory(DateTime pointInTime);

        IQueryable<TEntity> GetHistory(DateTime from, DateTime to);

        TEntity Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        void RemoveRange(params TEntity[] entities);

        TEntity Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        void UpdateRange(params TEntity[] entities);
    }
}
