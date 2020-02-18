using System;

namespace DataStore.Entities
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
        public int Active { get; set; }
    }
}