using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class ServiceRequest
    {
        public string Id { get; set; }
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastUpdatedBy { get; set; }
    }
}
