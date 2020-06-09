using System;

namespace QAEngine.Core.Exceptions
{
    public abstract class DomainException : Exception
    {
        public DomainException(string message, string errorCode)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public abstract int StatusCode { get; }

        public string ErrorCode { get; }
    }
}
