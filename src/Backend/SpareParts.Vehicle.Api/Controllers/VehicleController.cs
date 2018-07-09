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
using SpareParts.Repository;
using SpareParts.Vehicle.Api.Models.Vehicle;
using SpareParts.Vehicle.Cqrs.Commands;

namespace SpareParts.Vehicle.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : Controller
    {

        // GET api/vehicle
        [HttpGet]
        public IQueryable<GetModel> Get(
            [FromServices]IDataAccessObject<ReadModel.Vehicle> vehicleDataAccessObject,
            [FromServices]IMapper mapper)
        {
            return vehicleDataAccessObject.ProjectTo<GetModel>(mapper.ConfigurationProvider);
        }

        // GET api/vehicle
        [HttpGet("byPlate/{plate}")]
        public IQueryable<GetModel> GetByPlate(
            string plate,
            [FromServices]IDataAccessObject<ReadModel.Vehicle> vehicleDataAccessObject,
            [FromServices]IMapper mapper)
        {
            return vehicleDataAccessObject.Where(p => p.Plate.Contains(plate)).ProjectTo<GetModel>(mapper.ConfigurationProvider);
        }

        // DELETE api/vehicle/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromServices]IBus bus,
            string id)
        {
            var command = new RemoveVehicleCommand(id);

            await bus.Send(command);

            return Ok();
        }

        // GET api/vehicle/{id}
        [HttpGet("{id}")]
        public IActionResult Get(
            [FromServices]IDataAccessObject<ReadModel.Vehicle> vehicleDataAccessObject,
            [FromServices]IMapper mapper,
            string id)
        {
            GetModel result = vehicleDataAccessObject.Where(d => d.Id == id)
                .ProjectTo<GetModel>(mapper.ConfigurationProvider)
                .FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/vehicle
        [HttpPost]
        public async Task<IActionResult> Post(
                [FromServices] IBus bus,
                [Required]PostModel model)
        {
            // Hack: just for testing claims into the command handler
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, "test") }, "test"));

            var command = new AddVehicleCommand(
                NewId.Next().ToString(),
                model.Brand,
                model.Customer,
                model.Plate,
                model.Model,
                model.Color,
                model.Year);

            //await bus.Send(command);
            string vehicleId = await bus.SendRequest<string>(command, timeout: TimeSpan.FromMinutes(1));

            return CreatedAtAction("Get", new { id = vehicleId }, vehicleId);
        }

    }
}
