using System;
using System.Threading.Tasks;

namespace SpareParts.Repository
{
    public abstract class Repository<T> : IRepository<T>
        where T : class
    {
        protected abstract Task OnAddAsync(T entity);

        protected abstract Task OnUpdateAsync(T entity);

        protected abstract Task OnRemoveAsync(T entity);

        protected abstract Task<T> OnGetAsync(string id);

        #region IRepository<T>

        public Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            return OnAddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            return OnUpdateAsync(entity);
        }

        public Task RemoveAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            return OnRemoveAsync(entity);
        }

        public Task<T> GetAsync(string id)
        {
            return OnGetAsync(id);
        }

        #endregion

        #region IRepository

        public Task AddAsync(object entity)
        {
            return AddAsync(GetEntity(entity));
        }

        public Task UpdateAsync(object entity)
        {
            return AddAsync(GetEntity(entity));
        }

        public Task RemoveAsync(object entity)
        {
            return RemoveAsync(GetEntity(entity));
        }

        public async Task<object> GetObjectAsync(string id)
        {
            return await GetAsync(id);
        }

        #endregion

        private T GetEntity(object entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            var tentity = entity as T;
            if (tentity == null)
                throw new ArgumentException("Invalid entity type. Must be " + typeof(T));

            return tentity;
        }
    }
}
