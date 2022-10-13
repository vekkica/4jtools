using _4JTools.DataAccessLayer.Entities;
using _4JTools.DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace _4JTools.DataAccessLayer.DatabaseContext
{
    public class GenericDbContext : DbContext
    {
        public GenericDbContext(DbContextOptions options) : base(options)
        {
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(Set<TEntity>());
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess = true)
        {
            Timestamp();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            Timestamp();

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected void Timestamp()
        {
            ChangeTracker
                .Entries()
                .Where(x => x.Entity is IAuditable && (x.State == EntityState.Added || x.State == EntityState.Modified))
                .ToList()
                .ForEach(entry =>
                {
                    var entity = (IAuditable)entry.Entity;

                    if (entry.State == EntityState.Added)
                    {
                        entity.Created = DateTime.UtcNow;
                    }

                    entity.Modified = DateTime.UtcNow;
                });
        }
    }
}
