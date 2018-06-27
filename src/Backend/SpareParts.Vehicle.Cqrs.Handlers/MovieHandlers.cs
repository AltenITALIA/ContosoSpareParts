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
    public class MovieHandlers : IHandleMessages<AddMovieCommand>
    {
        private readonly ILogger<MovieHandlers> _logger;
        private readonly IBus _bus;
        private readonly IUserAccessor _userAccessor;
        private readonly IRepository<Movie> _repository;
        private readonly IDataAccessObject<ReadModel.Movie> _dataAccessObject;

        public MovieHandlers(
            ILogger<MovieHandlers> logger,
            IBus bus,
            IUserAccessor userAccessor,
            IRepository<Movie> repository,
            IDataAccessObject<ReadModel.Movie> dataAccessObject)
        {
            _logger = logger;
            _bus = bus;
            _userAccessor = userAccessor;
            _repository = repository;
            _dataAccessObject = dataAccessObject;
        }

        public async Task Handle(AddMovieCommand command)
        {
            if (await _dataAccessObject.AnyAsync(m => m.Id == command.Id))
            {
                _logger.LogWarning("Movie {title} already added", command.Title);

                await _bus.Publish(new MovieAddedEvent { MovieId = command.Id });
                return;
            }

            Message rawMessage = MessageContext.Current.Message;

            _logger.LogInformation("Add movie {title} requested by {user}", command.Title, _userAccessor.User?.Identity.Name);

            //using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //{
            //    transaction.EnlistRebus();

                var movie = new Movie(command.Id, command.Title)
                {
                    Year = command.Year
                };
                await _repository.AddAsync(movie);

                await _bus.Publish(new MovieAddedEvent { MovieId = command.Id });

            //    transaction.Complete();
            //}

            await _bus.Reply(command.Id);
        }

    }

}
