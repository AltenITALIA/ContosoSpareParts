using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpareParts.Repository;

namespace SpareParts.EntityFramework.Repository
{
    public class DbRepository<T, TDbContext> : Repository<T>
        where T : class
        where TDbContext : DbContext
    {
        public DbRepository(TDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TDbContext Context { get; set; }

        protected override async Task OnAddAsync(T entity)
        {
            DbSet.Add(entity);

            await Context.SaveChangesAsync();
        }

        protected override async Task OnUpdateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            await Context.SaveChangesAsync();
        }

        protected override async Task OnRemoveAsync(T entity)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        protected override Task<T> OnGetAsync(string id)
        {
            return Context.FindAsync<T>(id);
        }

        protected virtual DbSet<T> DbSet => Context.Set<T>();
    }
}
