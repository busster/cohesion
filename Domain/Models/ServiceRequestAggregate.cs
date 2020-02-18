using Domain.Exceptions;
using Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ServiceRequestAggregate : Entity<string>, IAggregateRoot
    {
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public ServiceRequestStatus Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastUpdatedBy { get; set; }
        public ServiceRequestAggregate(
            string id,
            string buildingCode,
            string description,
            int status,
            string createdBy,
            DateTime createdDate,
            string updatedBy,
            DateTime lastUpdated
        )
        {
            // Can make these 3 value objects
            if (buildingCode.Length == 0) throw new ServiceRequestDomainException("Building Code cannot be blank");

            if (description.Length == 0) throw new ServiceRequestDomainException("Description cannot be blank");

            if (createdBy.Length == 0) throw new ServiceRequestDomainException("Created By cannot be blank");

            Id = id;
            BuildingCode = buildingCode;
            Description = description;
            Status = (ServiceRequestStatus)status;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            LastModifiedBy = updatedBy;
            LastUpdatedBy = lastUpdated;
        }

        public void UpdateStatus (int status)
        {
            if (!Enum.IsDefined(typeof(ServiceRequestStatus), status))
            {
                throw new ServiceRequestDomainException("Status not valid");
            }
            Status = (ServiceRequestStatus)status;
        }
    }
}
