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
using SpareParts.Part.Api.Models.Part;
using SpareParts.Part.Cqrs.Commands;

namespace SpareParts.Part.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PartController : Controller
    {

        // GET api/part
        [HttpGet]
        public IQueryable<GetModel> Get(
            [FromServices]IDataAccessObject<Part.ReadModel.Part> partDataAccessObject,
            [FromServices]IMapper mapper)
        {
            return partDataAccessObject.ProjectTo<GetModel>(mapper.ConfigurationProvider);
        }

        // GET api/part/{code}
        [HttpGet("{code}")]
        public IActionResult Get(
            [FromServices]IDataAccessObject<Part.ReadModel.Part> partDataAccessObject,
            [FromServices]IMapper mapper,
            string code)
        {
            GetModel result = partDataAccessObject.Where(d => d.Code == code)
                .ProjectTo<GetModel>(mapper.ConfigurationProvider)
                .FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/part
        [HttpPost]
        public async Task<IActionResult> Post(
                [FromServices] IBus bus,
                [Required, FromBody]PostModel model)
        {
            // Hack: just for testing claims into the command handler
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, "test") }, "test"));

            var command = new AddPartCommand(model.Code, model.Name);

            await bus.Send(command);
            
            return CreatedAtAction("Get", new { code = model.Code}, model.Code);
        }

    }
}
