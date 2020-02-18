using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class CreateServiceRequest
    {
        public string buildingCode { get; set; }
        public string description { get; set; }
        public string createdBy { get; set; }
    }
}
