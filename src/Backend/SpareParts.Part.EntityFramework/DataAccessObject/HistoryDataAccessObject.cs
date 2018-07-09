using System.Linq;
using SpareParts.EntityFramework.DataAccessObject;
using SpareParts.Part.EntityFramework.DataModel;

namespace SpareParts.Part.EntityFramework.DataAccessObject
{
    internal class HistoryDataAccessObject : DataAccessObject<DomainModel.History, Part.ReadModel.History, PartEntities>
    {
        public HistoryDataAccessObject(PartEntities context) : base(context)
        {
        }

        protected override IQueryable<Part.ReadModel.History> Query => Context.Histories.Select(m => new Part.ReadModel.History
        {
            Id = m.Id,
            Date = m.Date,
            PartCode = m.PartCode,
            PhotoUri = m.PhotoUri,
            VehicleId = m.VehicleId
        });
    }
}
