using System.Linq;
using SpareParts.EntityFramework.DataAccessObject;
using SpareParts.Part.EntityFramework.DataModel;

namespace SpareParts.Part.EntityFramework.DataAccessObject
{
    internal class PartDataAccessObject : DataAccessObject<DomainModel.Part, Part.ReadModel.Part, PartEntities>
    {
        public PartDataAccessObject(PartEntities context) : base(context)
        {
        }

        protected override IQueryable<Part.ReadModel.Part> Query => Context.Parts.Select(m => new Part.ReadModel.Part
        {
            Code = m.Code,
            PhotoUri = m.PhotoUri,
            Name = m.Name
        });
    }
}
