using Microsoft.EntityFrameworkCore;

namespace _4JTools.DataAccessLayer.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(DbSet<TEntity> dbSet)
        {
            DbSet = dbSet;
        }

        public TEntity Add(TEntity entity) => DbSet.Add(entity).Entity;

        public void AddRange(IEnumerable<TEntity> entities) => DbSet.AddRange(entities);

        public void AddRange(params TEntity[] entities) => DbSet.AddRange(entities);

        public TEntity Attach(TEntity entity) => DbSet.Attach(entity).Entity;

        public void AttachRange(IEnumerable<TEntity> entities) => DbSet.AttachRange(entities);

        public void AttachRange(params TEntity[] entities) => DbSet.AttachRange(entities);

        public TEntity Find(params object[] keyValues) => DbSet.Find(keyValues);

        public ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken) => DbSet.FindAsync(keyValues, cancellationToken);

        public ValueTask<TEntity> FindAsync(params object[] keyValues) => DbSet.FindAsync(keyValues);

        public IQueryable<TEntity> Get(bool tracking = false)
        {
            return tracking ? DbSet.AsTracking() : DbSet.AsNoTracking();
        }

        public IQueryable<TEntity> GetHistory()
        {
            return DbSet.TemporalAll();
        }

        public IQueryable<TEntity> GetHistory(DateTime pointInTime)
        {
            return DbSet.TemporalAsOf(pointInTime);
        }

        public IQueryable<TEntity> GetHistory(DateTime from, DateTime to)
        {
            return DbSet.TemporalBetween(from, to);
        }

        public TEntity Remove(TEntity entity) => DbSet.Remove(entity).Entity;

        public void RemoveRange(IEnumerable<TEntity> entities) => DbSet.RemoveRange(entities);

        public void RemoveRange(params TEntity[] entities) => DbSet.RemoveRange(entities);

        public TEntity Update(TEntity entity) => DbSet.Update(entity).Entity;

        public void UpdateRange(IEnumerable<TEntity> entities) => DbSet.UpdateRange(entities);

        public void UpdateRange(params TEntity[] entities) => DbSet.UpdateRange(entities);
    }
}
