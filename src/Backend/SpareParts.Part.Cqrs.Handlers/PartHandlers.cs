using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;
using SpareParts.Common;
using SpareParts.DataAccessObject;
using SpareParts.Part.Cqrs.Commands;
using SpareParts.Part.Cqrs.Events;
using SpareParts.Repository;

namespace SpareParts.Part.Cqrs.Handlers
{
    public class PartHandlers : IHandleMessages<AddPartCommand>
    {
        private readonly ILogger<PartHandlers> _logger;
        private readonly IBus _bus;
        private readonly IUserAccessor _userAccessor;
        private readonly IRepository<DomainModel.Part> _repository;
        private readonly IDataAccessObject<Part.ReadModel.Part> _dataAccessObject;

        public PartHandlers(
            ILogger<PartHandlers> logger,
            IBus bus,
            IUserAccessor userAccessor,
            IRepository<DomainModel.Part> repository,
            IDataAccessObject<Part.ReadModel.Part> dataAccessObject)
        {
            _logger = logger;
            _bus = bus;
            _userAccessor = userAccessor;
            _repository = repository;
            _dataAccessObject = dataAccessObject;
        }

        public async Task Handle(AddPartCommand command)
        {
            if (await _dataAccessObject.AnyAsync(m => m.Code == command.Code))
            {
                _logger.LogWarning("Part {code} already added", command.Code);

                await _bus.Publish(new PartAddedEvent { PartCode = command.Code });
                return;
            }

            _logger.LogInformation("Add part with {code} requested by {user}", command.Code, _userAccessor.User?.Identity.Name);

            var part = new DomainModel.Part(command.Code, command.Name);
            await _repository.AddAsync(part);

            await _bus.Publish(new PartAddedEvent { PartCode = command.Code });
        }

    }

}
