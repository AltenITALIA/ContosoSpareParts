using System.Linq;
using SpareParts.EntityFramework.DataAccessObject;
using SpareParts.Vehicle.EntityFramework.DataModel;
using SpareParts.Vehicle.ReadModel;

namespace SpareParts.Vehicle.EntityFramework.DataAccessObject
{
    internal class MovieDataAccessObject : DataAccessObject<DomainModel.Movie, ReadModel.Movie, CatalogEntities>
    {
        public MovieDataAccessObject(CatalogEntities context) : base(context)
        {
        }

        protected override IQueryable<Movie> Query => Context.Movies.Select(m => new Movie
        {
            Id = m.Id,
            Title = m.Title,
            Year = m.Year
        });
    }
}
