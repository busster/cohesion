using DataStore.Entities;
using System.Collections.Generic;

namespace DataStore.Context
{
    public interface IServiceRequestDBContext
    {
        List<ServiceRequest> ServiceRequests { get; set; }
    }
}