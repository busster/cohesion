using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using DataStore.Context;
using Domain.Exceptions;
using Domain.Models;
using Infrastructure.Models;
using Infrastructure.Queries;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        private IServiceRequestRepository _repo;
        private IServiceRequestQuery _query;
        public ServiceRequestController(IServiceRequestRepository repo, IServiceRequestQuery serviceRequestQuery)
        {
            _repo = repo;
            _query = serviceRequestQuery;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var serviceRequests = await _query.GetAllAsync();
            if (serviceRequests.Count() > 0)
            {
                return Ok(serviceRequests);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var serviceRequest = await _query.GetAsync(id);
            if (serviceRequest == null)
            {
                return NotFound("Resource Not Found");
            }
            return Ok(serviceRequest);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateServiceRequest req)
        {
            string serviceRequestAggregateId = "";
            ServiceRequestAggregate serviceRequestAggregate;
            try
            {
                // 1. Try to create the aggregate
                string user = ""; // Pull this out of auth or something
                string guid = Guid.NewGuid().ToString();
                serviceRequestAggregate = new ServiceRequestAggregate(
                    guid,
                    req.buildingCode,
                    req.description,
                    (int)ServiceRequestStatus.Created,
                    req.createdBy,
                    DateTime.Now,
                    user,
                    DateTime.Now
                );

                // 2. Persist aggregate
                serviceRequestAggregateId = await _repo.CreateAsync(serviceRequestAggregate);
            }
            catch (ServiceRequestDomainException ex)
            {
                return BadRequest(ex.Message);
            }

            try
            {
                // 3. Sideaffects
                SendEmail(serviceRequestAggregate);
            }
            catch (Exception ex)
            {
                // log
            }
            return Created($"api/servicerequest/{serviceRequestAggregateId}", serviceRequestAggregate);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] UpdateServiceRequest req)
        {
            var serviceRequest = await _query.GetAsync(id);
            if (serviceRequest != null)
            {
                try
                {
                    // 1. try to rebuild aggregate
                    ServiceRequestAggregate serviceRequestAggregate = new ServiceRequestAggregate(
                        serviceRequest.Id,
                        serviceRequest.BuildingCode,
                        serviceRequest.Description,
                        serviceRequest.Status,
                        serviceRequest.CreatedBy,
                        serviceRequest.CreatedDate,
                        serviceRequest.LastModifiedBy,
                        serviceRequest.LastUpdatedBy
                    );

                    // 2. try to update status
                    //  I assumed this was the main thing to update?
                    //  if more needs to be updated the aggregate can be extended with methods to do so
                    serviceRequestAggregate.UpdateStatus(req.status);

                    // 2. persist new aggregate
                    await _repo.UpdateAsync(serviceRequestAggregate);

                    return Ok(serviceRequestAggregate);

                } catch (ServiceRequestDomainException ex)
                {
                    return BadRequest(ex.Message);
                }
            } else
            {
                return NotFound("Resource Not Found");
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var serviceRequest = await _query.GetAsync(id);
            if (serviceRequest != null)
            {
                try
                {
                    // 1. try to rebuild aggregate
                    ServiceRequestAggregate serviceRequestAggregate = new ServiceRequestAggregate(
                        serviceRequest.Id,
                        serviceRequest.BuildingCode,
                        serviceRequest.Description,
                        serviceRequest.Status,
                        serviceRequest.CreatedBy,
                        serviceRequest.CreatedDate,
                        serviceRequest.LastModifiedBy,
                        serviceRequest.LastUpdatedBy
                    );

                    // 2. Archive this aggregate
                    await _repo.ArchiveAsync(serviceRequestAggregate);

                    return Created($"api/servicerequest/{serviceRequestAggregate.Id}", serviceRequestAggregate);

                }
                catch (ServiceRequestDomainException ex)
                {
                    return BadRequest("Invalid request");
                }
            }
            else
            {
                return NotFound("Resource Not Found");
            }
        }

        private void SendEmail(ServiceRequestAggregate serviceRequest)
        {
            Console.WriteLine(serviceRequest);
        }
    }
}
