using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Context;
using Domain.Models;
using Infrastructure.Models;

namespace Infrastructure.Queries
{
    public class ServiceRequestQuery : IServiceRequestQuery
    {
        private IServiceRequestDBContext _context;
        public ServiceRequestQuery(IServiceRequestDBContext serviceRequestDBContext)
        {
            _context = serviceRequestDBContext;
        }

        public Task<List<ServiceRequest>> GetAllAsync()
        {
            return Task.Run(() => ActiveServiceRequests().Select(sr => Map(sr)).ToList());
        }

        public Task<ServiceRequest> GetAsync(string id)
        {
            return Task.Run(() => {
                var serviceRequest = ActiveServiceRequests().FirstOrDefault(sr => sr.Id == id);
                return serviceRequest == null ? null : Map(serviceRequest);
            });
        }

        private IEnumerable<DataStore.Entities.ServiceRequest> ActiveServiceRequests()
        {
            return _context.ServiceRequests.Where(sr => sr.Active == 1);
        }

        private ServiceRequest Map (DataStore.Entities.ServiceRequest serviceRequest)
        {
            return new ServiceRequest()
            {
                Id = serviceRequest.Id,
                BuildingCode = serviceRequest.BuildingCode,
                Description = serviceRequest.Description,
                Status = serviceRequest.Status,
                CreatedBy = serviceRequest.CreatedBy,
                CreatedDate = serviceRequest.CreatedDate,
                LastModifiedBy = serviceRequest.LastModifiedBy,
                LastUpdatedBy = serviceRequest.LastUpdatedBy
            };
        }
    }
}
