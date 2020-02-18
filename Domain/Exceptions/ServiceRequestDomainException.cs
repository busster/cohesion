using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class ServiceRequestDomainException : Exception
    {
        public ServiceRequestDomainException(string message) : base(message) { }
    }
}
