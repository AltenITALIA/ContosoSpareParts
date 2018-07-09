using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Messages;
using Rebus.Pipeline;
using SpareParts.Common;
using SpareParts.DataAccessObject;
using SpareParts.Repository;
using SpareParts.Vehicle.Cqrs.Commands;
using SpareParts.Vehicle.Cqrs.Events;
using SpareParts.Vehicle.DomainModel;

namespace SpareParts.Vehicle.Cqrs.Handlers
{
    public class VehicleHandlers : IHandleMessages<AddVehicleCommand>, IHandleMessages<RemoveVehicleCommand>
    {
        private readonly ILogger<VehicleHandlers> _logger;
        private readonly IBus _bus;
        private readonly IUserAccessor _userAccessor;
        private readonly IRepository<DomainModel.Vehicle> _repository;
        private readonly IDataAccessObject<ReadModel.Vehicle> _dataAccessObject;

        public VehicleHandlers(
            ILogger<VehicleHandlers> logger,
            IBus bus,
            IUserAccessor userAccessor,
            IRepository<DomainModel.Vehicle> repository,
            IDataAccessObject<ReadModel.Vehicle> dataAccessObject)
        {
            _logger = logger;
            _bus = bus;
            _userAccessor = userAccessor;
            _repository = repository;
            _dataAccessObject = dataAccessObject;
        }

        public async Task Handle(RemoveVehicleCommand command)
        {
            DomainModel.Vehicle vehicle = await _repository.GetAsync(command.Id);
            if (vehicle == null)
            {
                return;
            }
            await _repository.RemoveAsync(vehicle);
        }

        public async Task Handle(AddVehicleCommand command)
        {
            if (await _dataAccessObject.AnyAsync(m => m.Id == command.Id))
            {
                _logger.LogWarning("Vehicle {id} already added", command.Id);

                await _bus.Publish(new VehicleAddedEvent { VehicleId = command.Id });
                return;
            }

            _logger.LogInformation("Add vehicle with {plate} requested by {user}", command.Plate, _userAccessor.User?.Identity.Name);

            //using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //{
            //    transaction.EnlistRebus();

            var vehicle = new DomainModel.Vehicle(command.Id)
            {
                Plate = command.Plate,
                Model = command.Model,
                Brand = command.Brand,
                Color = command.Color,
                Customer = command.Customer,
                Year = command.Year,
            };
            await _repository.AddAsync(vehicle);

            await _bus.Publish(new VehicleAddedEvent { VehicleId = command.Id });

            //    transaction.Complete();
            //}

            await _bus.Reply(command.Id);
        }

    }

}
