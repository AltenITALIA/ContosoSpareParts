using System.Linq;
using SpareParts.EntityFramework.DataAccessObject;
using SpareParts.Vehicle.EntityFramework.DataModel;
using SpareParts.Vehicle.ReadModel;

namespace SpareParts.Vehicle.EntityFramework.DataAccessObject
{
    internal class VehicleDataAccessObject : DataAccessObject<DomainModel.Vehicle, ReadModel.Vehicle, VehicleEntities>
    {
        public VehicleDataAccessObject(VehicleEntities context) : base(context)
        {
        }

        protected override IQueryable<ReadModel.Vehicle> Query => Context.Vehicles.Select(m => new ReadModel.Vehicle
        {
            Id = m.Id,
            Model = m.Model,
            Color = m.Color,
            Customer = m.Customer,
            Brand = m.Brand,
            Plate = m.Plate,
            Year = m.Year
        });
    }
}
