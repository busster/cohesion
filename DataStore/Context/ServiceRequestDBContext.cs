using DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStore.Context
{
    public class ServiceRequestDBContext : IServiceRequestDBContext
    {
        public List<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
    }
}
