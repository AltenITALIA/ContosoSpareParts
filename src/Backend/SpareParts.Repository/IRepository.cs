using System.Threading.Tasks;

namespace SpareParts.Repository
{
    public interface IRepository
    {
        Task AddAsync(object entity);

        Task UpdateAsync(object entity);

        Task RemoveAsync(object entity);

        Task<object> GetObjectAsync(string id);
    }

    public interface IRepository<T> : IRepository
    {
        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);

        Task<T> GetAsync(string id);
    }
}
