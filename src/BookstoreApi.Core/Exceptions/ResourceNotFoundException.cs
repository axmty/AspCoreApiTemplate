using System;

namespace BookstoreApi.Core.Exceptions
{
    public class ResourceNotFoundException : DomainException
    {
        public ResourceNotFoundException(string message) : base(message)
        {
        }

        public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ResourceNotFoundException()
        {
        }
    }
}
