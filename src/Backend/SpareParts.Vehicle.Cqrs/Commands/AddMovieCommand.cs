using SpareParts.Cqrs;

namespace SpareParts.Vehicle.Cqrs.Commands
{
    public class AddMovieCommand : CommandBase
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public string Id { get; set; }
    }
}
