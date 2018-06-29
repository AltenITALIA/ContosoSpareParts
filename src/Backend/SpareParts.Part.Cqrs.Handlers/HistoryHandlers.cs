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
    public class HistoryHandlers : IHandleMessages<AddHistoryCommand>
    {
        private readonly ILogger<HistoryHandlers> _logger;
        private readonly IBus _bus;
        private readonly IUserAccessor _userAccessor;
        private readonly IRepository<DomainModel.History> _repository;
        private readonly IDataAccessObject<Part.ReadModel.History> _dataAccessObject;

        public HistoryHandlers(
            ILogger<HistoryHandlers> logger,
            IBus bus,
            IUserAccessor userAccessor,
            IRepository<DomainModel.History> repository,
            IDataAccessObject<Part.ReadModel.History> dataAccessObject)
        {
            _logger = logger;
            _bus = bus;
            _userAccessor = userAccessor;
            _repository = repository;
            _dataAccessObject = dataAccessObject;
        }

        public async Task Handle(AddHistoryCommand command)
        {
            if (await _dataAccessObject.AnyAsync(m => m.Id == command.Id))
            {
                _logger.LogWarning("Part history {code} for {vehicleId} already added", command.PartCode, command.VehicleId);

                await _bus.Publish(new HistoryAddedEvent { PartCode = command.PartCode, VehicleId = command.VehicleId });
                return;
            }

            _logger.LogInformation("Add part history with {code} for {vehicleId} requested by {user}", command.PartCode, command.VehicleId, _userAccessor.User?.Identity.Name);

            var part = new DomainModel.History(command.Id, command.PartCode, command.VehicleId);
            await _repository.AddAsync(part);

            await _bus.Publish(new HistoryAddedEvent { PartCode = command.PartCode, VehicleId = command.VehicleId });

            await _bus.Reply(command.Id);
        }

    }

}

