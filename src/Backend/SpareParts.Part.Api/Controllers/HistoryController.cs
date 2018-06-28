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
using SpareParts.Part.Api.Models.History;
using SpareParts.Part.Cqrs.Commands;

namespace SpareParts.Part.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoryController : Controller
    {

        // GET api/history
        [HttpGet]
        public IQueryable<GetModel> Get(
            [FromServices]IDataAccessObject<Part.ReadModel.History> dataAccessObject,
            [FromServices]IMapper mapper)
        {
            return dataAccessObject.ProjectTo<GetModel>(mapper.ConfigurationProvider);
        }

        // GET api/history/{vehicleId}
        [HttpGet("byVehicle/{vehicleId}")]
        public IQueryable<GetModel> Get(
            [FromServices]IDataAccessObject<Part.ReadModel.History> dataAccessObject,
            [FromServices]IMapper mapper,
            string vehicleId)
        {
            return dataAccessObject.Where(d => d.VehicleId == vehicleId).ProjectTo<GetModel>(mapper.ConfigurationProvider);
        }

        // POST api/history
        [HttpPost]
        public async Task<IActionResult> Post(
                [FromServices] IBus bus,
                [Required, FromBody]PostModel model)
        {
            // Hack: just for testing claims into the command handler
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, "test") }, "test"));

            var command = new AddHistoryCommand(NewId.Next().ToString(), model.PartCode, model.VehicleId);

            string id = await bus.SendRequest<string>(command);
            
            return CreatedAtAction("Get", new { id = id}, id);
        }

    }
}
