using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Rebus;
using Rebus.Bus;
using SpareParts.DataAccessObject;
using SpareParts.Part.Api.Models.History;
using SpareParts.Part.Cqrs.Commands;
using SpareParts.Part.ReadModel;
using SpareParts.Repository;

namespace SpareParts.Part.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoryController : Controller
    {
        // GET api/history
        [HttpGet]
        public IEnumerable<GetModel> Get(
            [FromServices] IConfiguration configuration,
            [FromServices] IDataAccessObject<History> dataAccessObject,
            [FromServices] IMapper mapper)
        {
            return ResolvePhotoUri(configuration, dataAccessObject.ProjectTo<GetModel>(mapper.ConfigurationProvider));
        }

        // GET api/history/{vehicleId}
        [HttpGet("byVehicle/{vehicleId}")]
        public IEnumerable<GetModel> Get(
            [FromServices] IConfiguration configuration,
            [FromServices] IDataAccessObject<History> dataAccessObject,
            [FromServices] IMapper mapper,
            string vehicleId)
        {
            return ResolvePhotoUri(configuration, dataAccessObject.Where(d => d.VehicleId == vehicleId)
                .ProjectTo<GetModel>(mapper.ConfigurationProvider));
        }

        private IEnumerable<GetModel> ResolvePhotoUri(IConfiguration configuration, IQueryable<GetModel> query)
        {
            var policy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTimeOffset.Now.AddMinutes(10)
            };
            CloudBlobContainer historyContainer = GetHistoryContainer(configuration);
            return query.AsEnumerable().Select(s =>
            {
                if (s.PhotoUri != null)
                {
                    CloudBlockBlob blob = historyContainer.GetBlockBlobReference(Path.GetFileName(s.PhotoUri));
                    string sas = blob.GetSharedAccessSignature(policy);

                    s.PhotoUri = blob.Uri + sas;
                }

                return s;
            });
        }

        // PUT api/history
        [HttpPut("photo/{id}")]
        public async Task<IActionResult> Put(
            string id,
            [FromServices] IConfiguration configuration,
            [FromServices] IRepository<DomainModel.History> repository)
        {
            DomainModel.History history = await repository.GetAsync(id);
            if (history == null) return NotFound();

            CloudBlobContainer historyContainer = GetHistoryContainer(configuration);

            CloudBlockBlob blob = historyContainer.GetBlockBlobReference(GetPhotoName(id));
            blob.Properties.ContentType = Request.ContentType ?? "application/octet-stream";
            await blob.UploadFromStreamAsync(Request.Body);

            history.PhotoUri = blob.Uri.ToString();
            await repository.UpdateAsync(history);

            return Created(blob.Uri, id);
        }

        private static string GetPhotoName(string id)
        {
            return $"{id}.jpg";
        }

        private static CloudBlobContainer GetHistoryContainer(IConfiguration configuration)
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(configuration.GetConnectionString("Storage"));
            CloudBlobClient client = account.CreateCloudBlobClient();
            CloudBlobContainer history = client.GetContainerReference("history");
            return history;
        }

        // POST api/history
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromServices] IBus bus,
            [Required]PostModel model)
        {
            // Hack: just for testing claims into the command handler
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[] {new Claim(ClaimsIdentity.DefaultNameClaimType, "test")}, "test"));

            var command = new AddHistoryCommand(NewId.Next().ToString(), model.PartCode, model.VehicleId);

            string id = await bus.SendRequest<string>(command);

            return CreatedAtAction("Get", new {id}, id);
        }
    }
}