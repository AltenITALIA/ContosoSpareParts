using System.Linq;

namespace SpareParts.DataAccessObject
{
    public interface IDataAccessObject : IQueryable
    {
    }

    public interface IDataAccessObject<out T> : IDataAccessObject, IQueryable<T>
    {

    }
}
