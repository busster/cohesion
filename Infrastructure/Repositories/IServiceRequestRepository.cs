using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IServiceRequestRepository
    {
        Task<string> CreateAsync(ServiceRequestAggregate serviceRequest);
        Task UpdateAsync(ServiceRequestAggregate serviceRequest);
        Task<string> ArchiveAsync(ServiceRequestAggregate serviceRequest);
    }
}