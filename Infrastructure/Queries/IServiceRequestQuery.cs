using Domain.Models;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
    public interface IServiceRequestQuery
    {
        Task<List<ServiceRequest>> GetAllAsync();
        Task<ServiceRequest> GetAsync(string id);
    }
}
