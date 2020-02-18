using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Context;
using DataStore.Entities;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private IServiceRequestDBContext _context;
        public ServiceRequestRepository (IServiceRequestDBContext serviceRequestDBContext)
        {
            _context = serviceRequestDBContext;
        }
        public Task<string> CreateAsync(ServiceRequestAggregate serviceRequest)
        {
            return Task.Run(() => {
                _context.ServiceRequests.Add(Map(serviceRequest));
                return serviceRequest.Id;
            });
        }

        public Task UpdateAsync(ServiceRequestAggregate serviceRequestAggregate)
        {
            return Task.Run(() => {
                var serviceRequest = GetById(serviceRequestAggregate.Id);
                if (serviceRequest == null)
                {
                    throw new Exception("Record Not Found");
                }
                else
                {
                    serviceRequest.Status = (int)serviceRequestAggregate.Status;
                }
            });
        }

        public Task<string> ArchiveAsync(ServiceRequestAggregate serviceRequestAggregate)
        {
            return Task.Run(() => {
                var serviceRequest = GetById(serviceRequestAggregate.Id);
                if (serviceRequest == null)
                {
                    throw new Exception("Record Not Found");
                }
                else
                {
                    serviceRequest.Active = 0;
                }
                return serviceRequestAggregate.Id;
            });
        }

        private ServiceRequest GetById (string id)
        {
            return _context.ServiceRequests.FirstOrDefault(sr => sr.Id == id);
        }

        private ServiceRequest Map(ServiceRequestAggregate serviceRequest)
        {
            return new ServiceRequest()
            {
                Id = serviceRequest.Id,
                BuildingCode = serviceRequest.BuildingCode,
                Description = serviceRequest.Description,
                Status = (int)serviceRequest.Status,
                CreatedBy = serviceRequest.CreatedBy,
                CreatedDate = serviceRequest.CreatedDate,
                LastModifiedBy = serviceRequest.LastModifiedBy,
                LastUpdatedBy = serviceRequest.LastUpdatedBy,
                Active = 1
            };
        }
    }
}
