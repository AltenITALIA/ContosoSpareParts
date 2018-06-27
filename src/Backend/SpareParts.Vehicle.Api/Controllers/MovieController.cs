using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Rebus;
using Rebus.Bus;
using SpareParts.DataAccessObject;
using SpareParts.Vehicle.Api.Models.Movie;
using SpareParts.Vehicle.Cqrs.Commands;

namespace SpareParts.Vehicle.Api.Controllers
{
    [Route("[controller]")]
    public class MovieController : Controller
    {

        // GET api/values
        [HttpGet]
        public IQueryable<GetModel> Get(
            [FromServices]IDataAccessObject<ReadModel.Movie> movieDataAccessObject,
            [FromServices]IMapper mapper)
        {
            return movieDataAccessObject.ProjectTo<GetModel>(mapper.ConfigurationProvider);
        }

        // GET suggestion/{id}
        [HttpGet("{id}")]
        public IActionResult Get(
            [FromServices]IDataAccessObject<ReadModel.Movie> suggestionsDataAccessObject,
            [FromServices]IMapper mapper,
            string id)
        {
            GetModel result = suggestionsDataAccessObject.Where(d => d.Id == id)
                .ProjectTo<GetModel>(mapper.ConfigurationProvider)
                .FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(
                [FromServices] IBus bus,
                [Required, FromBody]PostModel model)
        {
            // Hack: just for testing claims into the command handler
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, "test") }, "test"));

            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new AddMovieCommand
            {
                Id = NewId.Next().ToString(),
                Title = model.Title,
                Year = model.Year
            };

            //await bus.Send(command);
            string movieId = await bus.SendRequest<string>(command, timeout: TimeSpan.FromMinutes(1));

            return CreatedAtAction("Get", new { id = movieId });
        }

    }
}
